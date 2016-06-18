using UnityEngine;
using System.Collections;
using Weapons;
using SF.EventSystem;

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
			var targetPosition = TargetingBehavior.GetTarget();
			_weapon.TriggerPulled(targetPosition);
			SFEventManager.FireEvent(new EnemyAttackEventData { OriginId = _enemy.EntityId, TargetPosition = targetPosition });
		}
	}

	protected override void FinishBehavior()
	{
	}
}
