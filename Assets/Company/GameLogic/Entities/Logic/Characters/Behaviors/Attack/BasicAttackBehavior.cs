using UnityEngine;
using System.Collections;
using Weapons;

public class BasicAttackBehavior : AttackBehavior
{
	public BasicAttackBehavior(Enemy enemy, Weapon weapon) : base(enemy, weapon)
	{
	}

	protected override void StartBehavior()
	{
	}

	public override void UpdateBehavior()
	{
		HasTarget = TargetingBehavior.AcquireTarget();
		if(HasTarget)
		{
			_weapon.TriggerPulled(TargetingBehavior.GetTarget());
		}
	}

	protected override void FinishBehavior()
	{
	}
}
