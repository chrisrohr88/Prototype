using UnityEngine;

public class BasicMovementBehavior : BaseUpdateMovementBehavior
{
	public BasicMovementBehavior(float moveSpeed, GameObject gameObject)
	{
		_gameObject = gameObject;
		_moveSpeed = moveSpeed;
	}
	
	protected override void StartBehavior()
	{
	}
	
	public override void UpdateGameObject()
	{
		var position = _gameObject.transform.position;
		position.x -= _moveSpeed * Time.deltaTime;
		_gameObject.transform.position = position;
	}
	
	protected override void FinishBehavior()
	{
	}
}
