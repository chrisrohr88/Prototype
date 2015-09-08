using UnityEngine;

public class StaggerMovementBehavior : MovementBehavior
{
	private Vector3 _staggerTo;
	
	public StaggerMovementBehavior(Enemy enemy, float moveTimeLimit, System.Action callback) : base(enemy)
	{
		_moveTimeLimit = moveTimeLimit;
		_callback = callback;
		StartBehavior();
	}
	
	protected override void StartBehavior()
	{
		SetStaggerDirection();
	}
	
	private void SetStaggerDirection()
	{
		//TODO: Get bounds of fields and calulate movement
		_staggerTo = _enemy.EnemyRenderable.transform.position + new Vector3 (Random.Range(-10, 10), -1, 0);
	}
	
	public override void UpdateBehavior()
	{
		if(Vector3.Distance(_staggerTo, _enemy.EnemyRenderable.transform.position) > .5f)
		{
			_enemy.EnemyRenderable.transform.position = Vector3.MoveTowards(_enemy.EnemyRenderable.transform.position, _staggerTo, _moveSpeed * 0.1f);
		}
		else
		{
			FinishBehavior();
		}
	}
	
	protected override void FinishBehavior()
	{
		_callback.SafeInvoke();
	}
}
