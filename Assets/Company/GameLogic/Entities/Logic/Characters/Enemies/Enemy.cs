using UnityEngine;
using System.Collections;
using Weapons;

public class Enemy
{
	public Weapon Weapon { get; private set; }
	public HealthComponent Health { get; private set; }
	public TestEnemy TestEnemy { get; private set; }
	private float _speed = 50f;
	private CharacterBehavior _movementBehavior;
	private Weapon _weapon;
	public ModifiableAttribute BaseDamage { get; private set; } // TODO remove damage

	public float Damage
	{
		get
		{
			return 0;
		}
	}
	
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
		enemy.TestEnemy = (GameObject.Instantiate(Resources.Load(profile.EnemyPrefabPath)) as GameObject).GetComponent<TestEnemy>();
		enemy.TestEnemy.Enemy = enemy;
		enemy._weapon = Weapon.CreateFromProfile(ProfileManager.GetWeaponProfileByName(profile.WeaponProfileName), enemy.TestEnemy.SpwanTransform);
		enemy._movementBehavior = new BasicMovementBehavior(profile.Speed, enemy.TestEnemy.gameObject);
		return enemy;
	}

	private Enemy()
	{
//		Death += OnDeath;
	}

	protected void OnDeath()
	{
	}

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			_movementBehavior = new StaggerMovementBehavior(_speed, TestEnemy.gameObject, 5f, OnMovementBehaviorComplete);
		}
		if(Input.GetKeyDown(KeyCode.B))
		{
			_movementBehavior = new BlinkMovementBehavior(TestEnemy.gameObject, 5f, OnMovementBehaviorComplete);
		}
		_weapon.TriggerPulled();
		
		Move();
	}
	
	private void Move()
	{
		_movementBehavior.UpdateGameObject();
	}

	protected void OnMovementBehaviorComplete()
	{
		_movementBehavior = new BasicMovementBehavior(_speed, TestEnemy.gameObject);
	}
	
	public void UpdateHealth(float amount)
	{
		Health.UpdateHealth(amount);
	}
	
	public void UseWeapon()
	{
		Weapon.TriggerPulled();
	}
	
	~Enemy()
	{
		Death -= OnDeath;
	}
}
