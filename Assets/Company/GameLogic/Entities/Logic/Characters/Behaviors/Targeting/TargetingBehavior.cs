using UnityEngine;
using System.Collections;
using Weapons;

public abstract class TargetingBehavior : CharacterBehavior
{
	protected Vector3 _target;

	protected TargetingBehavior(Enemy enemy) : base(enemy)
	{
	}
	
	public abstract bool AcquireTarget();

	public Vector3 GetTarget()
	{
		return _target;
	}
	
	protected override sealed void StartBehavior()
	{
	}

	public override sealed void UpdateBehavior()
	{
	}

	protected override sealed void FinishBehavior()
	{
	}
}
