using UnityEngine;
using System.Collections;

public class BlinkMovementBehavior : BaseSingleUpdateMovementBehavior
{
	private Vector3 _blinkTo;

	// TODO min/max blink distance
	public BlinkMovementBehavior(GameObject gameObject, float blinkTime, System.Action callback)
	{
		_gameObject = gameObject;
		_moveTimeLimit = blinkTime;
		_callback = callback;
		StartBehavior ();
	}
	
	protected override void StartBehavior()
	{
		SetStaggerDirection();
		_gameObject.GetComponent<Renderer>().enabled = false;
		_gameObject.GetComponent<Collider2D>().enabled = false;
	}
	
	private void SetStaggerDirection()
	{
		//TODO: Get bounds of fields and calulate movement
		_blinkTo = _gameObject.transform.position + new Vector3 (Random.Range(-30, 30), -1, 0);
	}
	
	public override void UpdateGameObject()
	{
		//TODO Fade out
		_gameObject.transform.position = _blinkTo;
		//TODO Fade in
		FinishBehavior ();

	}
	
	protected override void FinishBehavior()
	{
		_gameObject.GetComponent<Renderer>().enabled = true;
		_gameObject.GetComponent<Collider2D>().enabled = true;
		_callback.SafeInvoke();
	}
}
