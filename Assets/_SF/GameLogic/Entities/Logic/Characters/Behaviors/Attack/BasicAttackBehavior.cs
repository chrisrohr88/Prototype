using SF.EventSystem;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.GameLogic.Entities.Logic.Weapons;
using SF.GameLogic.EventSystem.EventData;

namespace SF.GameLogic.Entities.Logic.Charaters.Behaviors.Attack
{
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
					_enemy.WeaponController.StartFiring(targetPosition);
					SFEventManager.FireEvent(new EnemyAttackEventData { OriginId = _enemy.EntityId, TargetPosition = targetPosition });
				}
				else
				{
					_enemy.WeaponController.StopFiring();
				}
			}
		}

		protected override void FinishBehavior()
		{
		}
	}
}
