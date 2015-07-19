using UnityEngine;

public abstract class MovementBehavior : CharacterBehavior
{
	protected float _moveSpeed;
	protected float _moveTimeLimit = -1;
	protected System.Action _callback;
}
