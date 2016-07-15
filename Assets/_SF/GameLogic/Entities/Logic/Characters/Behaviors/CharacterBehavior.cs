using SF.GameLogic.Entities.Logic.Charaters.Enemies;

namespace SF.GameLogic.Entities.Logic.Charaters.Behaviors
{
	public abstract class CharacterBehavior
	{
		protected Enemy _enemy;

		protected CharacterBehavior(Enemy enemy)
		{
			_enemy = enemy;
		}

		protected abstract void StartBehavior();
		public abstract void UpdateBehavior();
		protected abstract void FinishBehavior();
	}
}
