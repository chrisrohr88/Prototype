using UnityEngine;
using System.Collections;
using Weapons;

public class Enemy
{
	public Weapon Weapon { get; set; }
	public HealthComponent Health { get; set; }
	public BaseEnemy EnemyRenderable { get; set; }
	public float Speed { get; set; }
	public CharacterBehavior MovementBehavior { get; set; }
	public AttackBehavior AttackBehavior { get; set; }
	public LayerMask TargetingLayerMask { get; private set; }

	public readonly long id = 2;
	
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
		enemy.Weapon.EntityId = enemy.id;
		return enemy;
	}

	protected void OnDeath()
	{
	}

	public void Update()
	{
		AttackBehavior.UpdateBehavior();
		if(!AttackBehavior.HasTarget) {
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

	public void TakeDamage(DamageData damageData)
	{
		if(damageData.AttackerId != id)
		{
			UpdateHealth(-damageData.Damage);
		}
	}
}
