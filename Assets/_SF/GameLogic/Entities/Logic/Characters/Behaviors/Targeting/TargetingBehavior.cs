using UnityEngine;
using System.Collections;
using Weapons;

public abstract class TargetingBehavior
{
	protected Vector3 _target;

	public Enemy Enemy { get; set; }
	public bool HasTarget { get; protected set;}

	protected TargetingBehavior(Enemy enemy)
	{
		Enemy = enemy;
	}
	
	public abstract bool AcquireTarget();

	public Vector3 GetTarget()
	{
		return _target;
	}
}
