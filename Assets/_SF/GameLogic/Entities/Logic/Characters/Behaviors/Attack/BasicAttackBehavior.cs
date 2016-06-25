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
		if(TargetingBehavior != null)
		{
			HasTarget = TargetingBehavior.AcquireTarget();
			if(HasTarget)
			{
				var targetPosition = TargetingBehavior.GetTarget();
				SFEventManager.FireEvent(new EnemyAttackEventData { OriginId = _enemy.EntityId, TargetPosition = targetPosition });
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerRelease, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = targetPosition });

			}
		}
	}

	protected override void FinishBehavior()
	{
	}
}
