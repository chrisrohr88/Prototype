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
		LayerMask layerMask = 1 << LayerMask.NameToLayer("Player");
		var hit = Physics2D.Raycast(_enemy.EnemyRenderable.SpawnTransform.position, _enemy.EnemyRenderable.SpawnTransform.forward, 50, layerMask.value);
		return hit;
	}
}
