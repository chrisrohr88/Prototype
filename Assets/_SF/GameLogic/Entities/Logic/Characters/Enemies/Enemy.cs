using UnityEngine;
using System.Collections;
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
		enemy.AttackBehavior = CharacterBehaviorFactory.CreateAttackBehaviorFromType(profile.AttackBehaviorType, enemy, weapon);
		enemy.AttackBehavior.TargetingBehavior = CharacterBehaviorFactory.CreateTargetingBehaviorFromType(profile.TargetingBehaviorType, enemy);
		enemy.MovementBehavior = CharacterBehaviorFactory.CreateMovementBehaviorFromType(profile.MovementBehaviorType, enemy);
		enemy.Health = HealthComponent.Create(profile.BaseHealth);
		enemy.EnemyRenderable = baseEnemy;
		enemy.EnemyRenderable.Enemy = enemy;
		enemy.Speed = profile.Speed;
		enemy.Weapon = weapon;
		enemy.TargetingLayerMask = (LayerMask) profile.LayerMask;
		enemy.Weapon.EntityId = enemy.EntityId;
		enemy.PointValue = profile.PointValue;
		enemy.Health.Death += enemy.OnDeath;
		enemy.RegisterWithEventManager();
		return enemy;
	}

	private void RegisterWithEventManager()
	{
		SFEventManager.RegisterEvent(new SFEvent { OriginId = this.EntityId, EventType = SFEventType.EnemyDeath });
		SFEventManager.RegisterEvent(new SFEvent { OriginId = this.EntityId, EventType = SFEventType.EnemyAttack });
		SFEventManager.RegisterEventListner(SFEventType.EnemyHit, new ConcreteSFEventListner<EnemyHitEventData> { TargetId = this.EntityId, MethodToExecute = TakeDamage });
	}

	private Enemy() : base()
	{
	}

	protected void OnDeath()
	{
		SFEventManager.FireEvent(
			new EnemyDeathEventData 
			{ 
				OriginId = this.EntityId, 
				EventType = SFEventType.EnemyDeath, 
				PointValue = this.PointValue 
			});
		Health.Death -= OnDeath;
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

	public void TakeDamage(EnemyHitEventData eventData)
	{
		if(eventData.DamageData.AttackerId != EntityId)
		{
			UpdateHealth(-eventData.DamageData.Damage);
		}
	}
}
