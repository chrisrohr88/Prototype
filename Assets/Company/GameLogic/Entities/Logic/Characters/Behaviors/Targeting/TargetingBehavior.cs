using UnityEngine;
using System.Collections;
using Weapons;

public abstract class TargetingBehavior
{
	protected BaseEnemy _gameObject;
	protected Vector3 _target;
	
	public abstract bool AcquireTarget();

	public Vector3 GetTarget()
	{
		return _target;
	}
}
