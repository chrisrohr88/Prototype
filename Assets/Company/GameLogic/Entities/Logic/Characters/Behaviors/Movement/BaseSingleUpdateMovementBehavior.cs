using UnityEngine;

public abstract class BaseSingleUpdateMovementBehavior : SingleUpdateActionBehavior
{
	protected float _moveSpeed;
	protected float _moveTimeLimit = -1;
}
