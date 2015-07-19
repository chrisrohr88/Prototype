using UnityEngine;
using System.Collections;
using Weapons;

public class SimpleTagetingBehavior : TargetingBehavior
{
	public SimpleTagetingBehavior(BaseEnemy enemy)
	{
		_gameObject = enemy;
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
		var hit = Physics2D.Raycast(_gameObject.SpawnTransform.position, _gameObject.SpawnTransform.forward, 50, layerMask.value);
		Debug.DrawRay(_gameObject.SpawnTransform.position, _gameObject.SpawnTransform.forward * 50, Color.red, 1);
		return hit;
	}
}
