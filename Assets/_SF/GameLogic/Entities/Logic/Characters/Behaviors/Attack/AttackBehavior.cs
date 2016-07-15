using SF.GameLogic.Entities.Logic.Charaters.Behaviors.Targeting;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.GameLogic.Entities.Logic.Weapons;

namespace SF.GameLogic.Entities.Logic.Charaters.Behaviors.Attack
{
	public abstract class AttackBehavior : CharacterBehavior
	{
		protected Weapon _weapon;
		public TargetingBehavior TargetingBehavior { get; set; }
		public bool HasTarget { get; protected set; }

		protected AttackBehavior(Enemy enemy, Weapon weapon) : base(enemy)
		{
			_weapon = weapon;
		}
	}
}
