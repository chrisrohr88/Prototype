using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.GameLogic.Entities.Logic.Charaters.Behaviors;

namespace SF.GameLogic.Entities.Logic.Charaters.Behaviors.Movement
{
	public abstract class MovementBehavior : CharacterBehavior
	{
		protected float _moveSpeed;
		protected float _moveTimeLimit = -1;
		protected System.Action _callback;

		protected MovementBehavior(Enemy enemey) : base(enemey)
		{
			_moveSpeed = enemey.Speed;
		}
	}
}
