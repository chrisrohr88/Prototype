using SF.GameLogic.Entities.Logic.Charaters.Behaviors.Targeting;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.GameLogic.Entities.Logic.Weapons;
using SF.CustomInspector.Attributes;

namespace SF.GameLogic.Entities.Logic.Charaters.Behaviors.Attack
{
	public abstract class AttackBehavior : CharacterBehavior
	{
		protected Weapon _weapon;
		[InspectorObject] public TargetingBehavior TargetingBehavior { get; set; }
		public bool HasTarget { get; protected set; }

		protected AttackBehavior(Enemy enemy, Weapon weapon) : base(enemy)
		{
			_weapon = weapon;
		}
	}
}
