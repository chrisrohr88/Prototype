using UnityEngine;
using System.Collections;
using Weapons;

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
		HasTarget = TargetingBehavior.AcquireTarget();
		if(HasTarget)
		{
			_weapon.TriggerPulled(TargetingBehavior.GetTarget());
		}
	}

	protected override void FinishBehavior ()
	{
	}
}
