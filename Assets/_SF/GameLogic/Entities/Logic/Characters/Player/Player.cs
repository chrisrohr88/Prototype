using System.Collections;
using UnityEngine;
using SF.EventSystem;
using System.Collections.Generic;
using SF.GameLogic.Data.Profiles;
using SF.GameLogic.Controls.Interactables;
using SF.GameLogic.Entities.Logic.Weapons;
using SF.GameLogic.Entities.Logic.Components;
using SF.GameLogic.EventSystem.EventData;
using SF.GameLogic.EventSystem.EventRegistrars;
using SF.Utilities.Managers;

namespace SF.GameLogic.Entities.Logic.Charaters.Player
{
	public class Player : Character
	{
	    public Weapon Weapon { get; private set; }
		public int id = 1;

	    public static Player Create(float baseHealth)
	    {
			var player = new Player();
			player._eventRegistar = new PlayerEventRegistrar(player);
			player.Health = HealthComponent.Create(baseHealth);
			player.Health.Death += player.OnDeath;
	        player.PickupWeapon();
	        return player;
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
			Health.Death -= OnDeath;
			SFEventManager.FireEvent(new SFEventData { OriginId = EntityId, EventType = SFEventType.PlayerDeath });
			base.OnDeath();
		}
	}
}
