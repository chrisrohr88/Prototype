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
	public ModifiableAttribute BaseDamage { get; private set; }

	public float Damage
	{
		get
		{
			return BaseDamage.ModifiedValue;
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
	
	public static Enemy Create(float baseHealth, string enemyPath)
	{
		var enemy = new Enemy(baseHealth, enemyPath);
		return enemy;
	}

	// TODO make from profile
	private Enemy(float baseHealth, string enemyPath)
	{
		BaseDamage = ModifiableAttribute.Create(25);
		Health = HealthComponent.Create(baseHealth);
		Death += () => { Debug.Log ("Enemy is dead!"); };
		TestEnemy = (GameObject.Instantiate(Resources.Load(enemyPath)) as GameObject).GetComponent<TestEnemy>();
		Debug.Log (TestEnemy + enemyPath);
		TestEnemy.Enemy = this;
		_movementBehavior = new BasicMovementBehavior(_speed, TestEnemy.gameObject);
		_weapon = Weapon.CreateFromProfile(ProfileManager.GetWeaponProfileByName("AI"), TestEnemy.SpwanTransform);
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
	}
}
