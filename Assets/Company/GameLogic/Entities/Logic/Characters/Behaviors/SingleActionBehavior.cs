using UnityEngine;

public abstract class SingleActionBehavior : CharacterBehavior
{
	protected System.Action _callback;

	protected abstract void PerformAction();
	
	protected override sealed void StartBehavior()
	{
		PerformAction();
	}
	
	public sealed override void UpdateGameObject()
	{
		//Do nothing
	}
	
	protected sealed override void FinishBehavior()
	{
		//Do nothing
	}
}
