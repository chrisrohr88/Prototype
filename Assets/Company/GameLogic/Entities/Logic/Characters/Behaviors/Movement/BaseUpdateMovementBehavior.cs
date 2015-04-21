using UnityEngine;

public abstract class BaseUpdateMovementBehavior : CharacterBehavior
{
	protected float _moveSpeed;
	protected float _moveTimeLimit = -1;
}
