﻿using UnityEngine;
using System.Collections;
using System;

public class BaseProjectile : MonoBehaviour
{
	public Projectile Projectile { get; set; }

    protected void OnDestroy()
	{
    }

	protected virtual void Update()
	{
		transform.localPosition += Projectile.Velocity * Projectile.Speed * Time.deltaTime;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		var baseEnemy = other.gameObject.GetComponent<BaseEnemy>();

		if(baseEnemy != null && baseEnemy.Enemy.EntityId != Projectile.DamageData.AttackerId)
		{
			Projectile.DamageData.TargetId = baseEnemy.Enemy.EntityId;
			Projectile.TryToFireEvent();
			return;
		}

		var basePlayer = other.gameObject.GetComponent<PlayerWall>();

		if(basePlayer != null && basePlayer.Player.EntityId != Projectile.DamageData.AttackerId)
		{
			Projectile.DamageData.TargetId = basePlayer.Player.EntityId;
			Projectile.TryToFireEvent();
			return;
		}
	}
}
