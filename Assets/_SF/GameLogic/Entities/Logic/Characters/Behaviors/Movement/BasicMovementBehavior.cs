using UnityEngine;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;

namespace SF.GameLogic.Entities.Logic.Charaters.Behaviors.Movement
{
	public class BasicMovementBehavior : MovementBehavior
	{
		public BasicMovementBehavior(Enemy enemy) : base(enemy) 
		{
		}
		
		protected override void StartBehavior()
		{
		}
		
		public override void UpdateBehavior()
		{
			var position = _enemy.EnemyRenderable.transform.position;
			position.x -= _enemy.EnemyRenderable.Enemy.Speed * Time.deltaTime;
			_enemy.EnemyRenderable.transform.position = position;
		}
		
		protected override void FinishBehavior()
		{
		}
	}
}
