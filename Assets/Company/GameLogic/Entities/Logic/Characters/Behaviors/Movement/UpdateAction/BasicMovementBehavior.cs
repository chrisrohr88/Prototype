using UnityEngine;

public class BasicMovementBehavior : BaseUpdateMovementBehavior
{
	public BasicMovementBehavior(float moveSpeed, BaseEnemy gameObject)
	{
		_gameObject = gameObject;
		_moveSpeed = moveSpeed;
	}
	
	protected override void StartBehavior()
	{
	}
	
	public override void UpdateBehavior()
	{
		var position = _gameObject.transform.position;
		position.x -= _gameObject.Enemy.Speed * Time.deltaTime;
		_gameObject.transform.position = position;
	}
	
	protected override void FinishBehavior()
	{
	}
}
