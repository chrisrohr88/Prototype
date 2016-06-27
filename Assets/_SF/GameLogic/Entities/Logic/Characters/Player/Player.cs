using System.Collections;
using UnityEngine;
using Weapons;
using SF.EventSystem;
using System.Collections.Generic;

public class Player : Entity
{
    public Weapon Weapon { get; private set; }
	public HealthComponent Health { get; private set; }
	public int id = 1;

    public static Player Create(float baseHealth)
    {
        var player = new Player();
		player.Health = HealthComponent.Create(baseHealth);
		player.Health.Death += player.OnDeath;
        player.PickupWeapon();
		player.SetupEventRegistar();
        return player;
	}

	// TODO : want to make this datadriven & move to Entity
	private	List<SFEvent> CreateEventsToRegister()
	{
		return new List<SFEvent>
		{
			new SFEvent { OriginId = EntityId, EventType = SFEventType.EntityHit },
			new SFEvent { OriginId = EntityId, EventType = SFEventType.PlayerDeath }
		};
	}

	private void SetupEventRegistar()
	{
		_eventRegistar = new EventRegistar(CreateEventsToRegister());
		_eventRegistar.RegisterEvents();
		SFEventManager.RegisterEventListner(SFEventType.EntityHit, new ConcreteSFEventListner<EntityHitEventData> { TargetId = EntityId, MethodToExecute = TakeDamage });
	}

    private Player() : base()
    {
		SubscribeEvents();
    }

	public void PickupWeapon(string profileName)
	{
		PickupWeapon(ProfileManager.GetWeaponProfileByName(profileName));
	}

    public void PickupWeapon()
    {
        PickupWeapon(ProfileManager.GetRandomWeapon());
	}
	
	public void PickupWeapon(WeaponProfile profile)
	{
		Weapon = WeaponFactory.CreateFromProfile(profile, GameManager.Instance.GameMode.FireTransform);
		Weapon.PlayerEntityId = EntityId;
		Debug.Log("Weapon is " + Weapon.Name);
	} 

	public void TriggerPulled(Vector3 position)
	{
		SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerPull, OriginId = Weapon.EntityId, TargetId = Weapon.EntityId, TargetPosition = position });
	}
	
	public void TriggerHeld(Vector3 position)
	{
		SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerHold, OriginId = Weapon.EntityId, TargetId = Weapon.EntityId, TargetPosition = position });
	}
	
	public void TriggerReleased(Vector3 position)
	{
		SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerRelease, OriginId = Weapon.EntityId, TargetId = Weapon.EntityId, TargetPosition = position });
	}

	public void TakeDamage(EntityHitEventData eventData)
	{
		Debug.Log("Player Hit");
		if(eventData.DamageData.AttackerId != EntityId)
		{
			Debug.Log("Player Health Updated by " + eventData.DamageData.Damage);
			Health.UpdateHealth(-eventData.DamageData.Damage);
			Debug.Log("Player Health is " + Health.TestHealth);
		}
	}
	
	private void SubscribeEvents()
	{
		UnsubscribeEvents();
		FieldInteractable.OnHeld += TriggerHeld;
		FieldInteractable.OnMoved += TriggerHeld;
		FieldInteractable.OnPressed += TriggerPulled;
		FieldInteractable.OnReleased += TriggerReleased;
	}
	
	public void UnsubscribeEvents()
	{
		FieldInteractable.OnHeld -= TriggerHeld;
		FieldInteractable.OnMoved -= TriggerHeld;
		FieldInteractable.OnPressed -= TriggerPulled;
		FieldInteractable.OnReleased -= TriggerReleased;
	}

	protected override void OnDeath()
	{
		SFEventManager.FireEvent(new SFEventData { OriginId = EntityId, EventType = SFEventType.PlayerDeath });
		base.OnDeath();
	}

    ~Player()
	{
		UnsubscribeEvents();
	}
}
