using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Weapons;
using SF.EventSystem;

public class Enemy : Entity
{
	public Weapon Weapon { get; set; }
	public HealthComponent Health { get; set; }
	public BaseEnemy EnemyRenderable { get; set; }
	public float Speed { get; set; }
	public CharacterBehavior MovementBehavior { get; set; }
	public AttackBehavior AttackBehavior { get; set; }
	public LayerMask TargetingLayerMask { get; private set; }
	public int PointValue { get; private set; }
	
	public event System.Action Death
	{
		add
		{
			Health.Death += value;
		}
		remove
		{
			Health.Death -= value;
		}
	}

	public static Enemy Create(EnemyProfile profile, Weapon weapon, BaseEnemy baseEnemy)
	{
		var enemy = new Enemy();
		enemy.MovementBehavior = CharacterBehaviorFactory.CreateMovementBehaviorFromType(profile.MovementBehaviorType, enemy);
		enemy.Speed = profile.Speed;
		enemy.TargetingLayerMask = (LayerMask) profile.LayerMask;
		enemy.PointValue = profile.PointValue;
		enemy.Health = HealthComponent.Create(profile.BaseHealth);
		enemy.Health.Death += enemy.OnDeath;

		if(weapon != null)
		{
			enemy.AttackBehavior = CharacterBehaviorFactory.CreateAttackBehaviorFromType(profile.AttackBehaviorType, enemy, weapon);
			enemy.AttackBehavior.TargetingBehavior = CharacterBehaviorFactory.CreateTargetingBehaviorFromType(profile.TargetingBehaviorType, enemy);
			enemy.Weapon = weapon;
			enemy.Weapon.PlayerEntityId = enemy.EntityId;
		}

		if(baseEnemy != null)
		{
			enemy.EnemyRenderable = baseEnemy;
			enemy.EnemyRenderable.Enemy = enemy;
		}

		enemy.SetupEventRegistar();
		return enemy;
	}

	// TODO : want to make this datadriven & move to Entity
	private	List<SFEvent> CreateEventsToRegister()
	{
		return new List<SFEvent>
		{
			new SFEvent { OriginId = EntityId, EventType = SFEventType.EnemyDeath },
			new SFEvent { OriginId = EntityId, EventType = SFEventType.EntityHit },
			new SFEvent { OriginId = EntityId, EventType = SFEventType.EntityAttack }
		};
	}

	private void SetupEventRegistar()
	{
		_eventRegistar = new EventRegistar(CreateEventsToRegister());
		_eventRegistar.RegisterEvents();
		SFEventManager.RegisterEventListner(SFEventType.EntityHit, new ConcreteSFEventListner<EntityHitEventData> { TargetId = this.EntityId, MethodToExecute = TakeDamage });
	}

	private Enemy() : base()
	{
	}

	protected override void OnDeath()
	{
		SFEventManager.FireEvent(
			new EnemyDeathEventData 
			{ 
				OriginId = this.EntityId, 
				EventType = SFEventType.EnemyDeath, 
				PointValue = this.PointValue 
			});
		Health.Death -= OnDeath;
		base.OnDeath();
	}

	public void Update()
	{
		AttackBehavior.UpdateBehavior();
		if(!AttackBehavior.HasTarget)
		{
			MovementBehavior.UpdateBehavior();
		}
	}

	protected void OnMovementBehaviorComplete()
	{
		MovementBehavior = new BasicMovementBehavior(this);
	}
	
	public void UpdateHealth(float amount)
	{
		Health.UpdateHealth(amount);
	}

	public void TakeDamage(EntityHitEventData eventData)
	{
		if(eventData.DamageData.AttackerId != EntityId)
		{
			UpdateHealth(-eventData.DamageData.Damage);
		}
	}
}
