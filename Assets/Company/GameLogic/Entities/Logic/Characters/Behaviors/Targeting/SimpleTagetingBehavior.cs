using UnityEngine;
using System.Collections;
using Weapons;

public class SimpleTagetingBehavior : TargetingBehavior
{
	public SimpleTagetingBehavior(Enemy enemy) : base(enemy)
	{
	}

	public override bool AcquireTarget()
	{
		var hit = doRaycast();
		return EvaluateHit(hit);
	}

	private bool EvaluateHit(RaycastHit2D hit)
	{
		if(hit.transform != null)
		{
			_target = hit.transform.position;
			return true;
		}
		return false;
	}

	private RaycastHit2D doRaycast()
	{
		var hit = Physics2D.Raycast(Enemy.EnemyRenderable.SpawnTransform.position, Enemy.EnemyRenderable.SpawnTransform.forward, 50, Enemy.TargetingLayerMask.value);
		return hit;
	}
}
