using UnityEngine;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.Utilities.Extensions;

namespace SF.GameLogic.Entities.Logic.Charaters.Behaviors.Movement
{
	public class BlinkMovementBehavior : MovementBehavior
	{
		private Vector3 _blinkTo;

		// TODO min/max blink distance
		public BlinkMovementBehavior(Enemy enemy, float blinkTime, System.Action callback) : base(enemy)
		{
			_moveTimeLimit = blinkTime;
			_callback = callback;
			StartBehavior();
		}
		
		protected override void StartBehavior()
		{
			SetStaggerDirection();
			_enemy.EnemyRenderable.GetComponent<Renderer>().enabled = false;
			_enemy.EnemyRenderable.GetComponent<Collider2D>().enabled = false;
		}
		
		private void SetStaggerDirection()
		{
			//TODO: Get bounds of fields and calulate movement
			_blinkTo = _enemy.EnemyRenderable.transform.position + new Vector3 (Random.Range(-30, 30), -1, 0);
		}
		
		public override void UpdateBehavior()
		{
			//TODO Fade out
			_enemy.EnemyRenderable.transform.position = _blinkTo;
			//TODO Fade in
			FinishBehavior();

		}
		
		protected override void FinishBehavior()
		{
			_enemy.EnemyRenderable.GetComponent<Renderer>().enabled = true;
			_enemy.EnemyRenderable.GetComponent<Collider2D>().enabled = true;
			_callback.SafeInvoke();
		}
	}
}
