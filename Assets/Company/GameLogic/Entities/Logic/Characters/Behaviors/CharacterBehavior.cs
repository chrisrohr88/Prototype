using UnityEngine;
using System.Collections;
using Weapons;

public enum MovementBehaviorType
{
	BasicMovement,
	Blink, 
	Stagger
}

public abstract class CharacterBehavior
{
	protected BaseEnemy _gameObject;
	public CharacterBehavior NextBehavior { get; protected set; }
	
	protected abstract void StartBehavior();
	public abstract void UpdateBehavior();
	protected abstract void FinishBehavior();
}

public abstract class TargetingBehavior
{
	protected BaseEnemy _gameObject;
	protected Vector3 _target;
	
	public abstract bool AcquireTarget();

	public Vector3 GetTarget()
	{
		return _target;
	}
}

public class SimpleTagetingBehavior : TargetingBehavior
{
	public SimpleTagetingBehavior(BaseEnemy enemy)
	{
		_gameObject = enemy;
	}

	public override bool AcquireTarget()
	{
		// TODO -- Finish targeting
		Physics2D.Raycast(_gameObject.SpawnTransform.position, _gameObject.SpawnTransform.forward, 50);
		Debug.DrawRay(_gameObject.SpawnTransform.position, _gameObject.SpawnTransform.forward, Color.red, 50);
		return true;
	}
}

public abstract class AttackBehavior : CharacterBehavior
{
	protected Weapon _weapon;
	public TargetingBehavior TargetingBehavior { get; set; }
}

public class SimpleAttackBehavior : AttackBehavior
{
	public SimpleAttackBehavior(BaseEnemy enemy, Weapon weapon)
	{
		_weapon = weapon;
		_gameObject = enemy;
		TargetingBehavior = new SimpleTagetingBehavior(enemy);
	}

	protected override void StartBehavior ()
	{
	}

	public override void UpdateBehavior ()
	{
		if(TargetingBehavior.AcquireTarget())
		{

			_weapon.TriggerPulled(new Vector3(0,0,0));
		}
	}

	protected override void FinishBehavior ()
	{
	}
}


