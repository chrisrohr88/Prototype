using UnityEngine;

public class StaggerMovementBehavior : BaseSingleUpdateMovementBehavior
{
	private Vector3 _staggerTo;
	
	public StaggerMovementBehavior(float moveSpeed, GameObject gameObject, float moveTimeLimit, System.Action callback)
	{
		_gameObject = gameObject;
		_moveSpeed = moveSpeed;
		_moveTimeLimit = moveTimeLimit;
		_callback = callback;
		StartBehavior ();
	}
	
	protected override void StartBehavior()
	{
		SetStaggerDirection();
	}
	
	private void SetStaggerDirection()
	{
		//TODO: Get bounds of fields and calulate movement
		_staggerTo = _gameObject.transform.position + new Vector3 (Random.Range(-10, 10), -1, 0);
	}
	
	public override void UpdateGameObject()
	{
		if(Vector3.Distance(_staggerTo, _gameObject.transform.position) > .5f)
		{
			_gameObject.transform.position = Vector3.MoveTowards(_gameObject.transform.position, _staggerTo, _moveSpeed * 0.1f);
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
