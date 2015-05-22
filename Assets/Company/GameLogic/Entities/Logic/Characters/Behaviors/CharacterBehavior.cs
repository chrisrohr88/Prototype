using UnityEngine;
using System.Collections;

public enum MovementBehaviorType // MovementBehavior?
{
	BasicMovement,
	Blink,
	Stagger
}

public abstract class CharacterBehavior
{
	protected BaseEnemy _gameObject;
	protected CharacterBehavior _nextBehavior;
	
	protected abstract void StartBehavior();
	public abstract void UpdateGameObject();
	protected abstract void FinishBehavior();
}
