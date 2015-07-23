using UnityEngine;
using System.Collections;
using Weapons;

public class BasicAttackBehavior : AttackBehavior
{
	public BasicAttackBehavior(Enemy enemy, Weapon weapon) : base(enemy, weapon)
	{
		TargetingBehavior = new SimpleTagetingBehavior(enemy);
	}

	protected override void StartBehavior()
	{
	}

	public override void UpdateBehavior()
	{
		TargetingBehavior.UpdateBehavior();
		if(TargetingBehavior.HasTarget)
		{
			_weapon.TriggerPulled(TargetingBehavior.GetTarget());
		}
	}

	protected override void FinishBehavior()
	{
	}
}
