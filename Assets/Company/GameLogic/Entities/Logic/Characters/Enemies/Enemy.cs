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
		var enemy = new Enemy();
		enemy.Health = HealthComponent.Create(profile.BaseHealth);
		enemy.EnemyRenderable = (GameObject.Instantiate(Resources.Load(profile.EnemyPrefabPath)) as GameObject).GetComponent<BaseEnemy>();
		enemy.EnemyRenderable.Enemy = enemy;
		enemy.Speed = profile.Speed;
		enemy.Weapon = WeaponFactory.CreateFromProfile(ProfileManager.GetWeaponProfileByName(profile.WeaponProfileName), enemy.EnemyRenderable.SpawnTransform);
		enemy._movementBehavior = new BasicMovementBehavior(enemy.Speed, enemy.EnemyRenderable);
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
		if(Input.GetKeyDown(KeyCode.Q))
		{
			_movementBehavior = new StaggerMovementBehavior(Speed, EnemyRenderable, 5f, OnMovementBehaviorComplete);
		}
		if(Input.GetKeyDown(KeyCode.B))
		{
			_movementBehavior = new BlinkMovementBehavior(EnemyRenderable, 5f, OnMovementBehaviorComplete);
		}
		UseWeapon();
		
		Move();
	}
	
	private void Move()
	{
		_movementBehavior.UpdateGameObject();
	}

	protected void OnMovementBehaviorComplete()
	{
		_movementBehavior = new BasicMovementBehavior(Speed, EnemyRenderable);
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
	
	public void UseWeapon()
	{
		Weapon.TriggerPulled();
	}
}
