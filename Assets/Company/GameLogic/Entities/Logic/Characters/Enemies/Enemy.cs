using UnityEngine;
using System.Collections;
using Weapons;

public class Enemy
{
	public Weapon Weapon { get; private set; }
	public HealthComponent Health { get; private set; }
	public BaseEnemy EnemyRenderable { get; private set; }
	public float Speed { get; private set; }
	private CharacterBehavior _movementBehavior;
	private AttackBehavior _attackBehavior;

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
	
	public static Enemy Create(EnemyProfile profile)
	{
		BaseEnemy baseEnemy = (GameObject.Instantiate(Resources.Load(profile.EnemyPrefabPath)) as GameObject).GetComponent<BaseEnemy>();
		Weapon weapon = WeaponFactory.CreateFromProfile(ProfileManager.GetWeaponProfileByName(profile.WeaponProfileName), baseEnemy.SpawnTransform);
		var enemy = new Enemy();
		enemy._attackBehavior = new BasicAttackBehavior(enemy, weapon);
		enemy.Health = HealthComponent.Create(profile.BaseHealth);
		enemy.EnemyRenderable = baseEnemy;
		enemy.EnemyRenderable.Enemy = enemy;
		enemy.Speed = profile.Speed;
		enemy.Weapon = weapon;
		enemy._movementBehavior = new BasicMovementBehavior(enemy);
		enemy.Weapon.EntityId = enemy.id;
		return enemy;
	}

	private Enemy()
	{
	}

	protected void OnDeath()
	{
	}

	public void Update()
	{
		_attackBehavior.UpdateBehavior();
		if(!_attackBehavior.HasTarget) {
			_movementBehavior.UpdateBehavior();
		}
	}

	protected void OnMovementBehaviorComplete()
	{
		_movementBehavior = new BasicMovementBehavior(this);
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
