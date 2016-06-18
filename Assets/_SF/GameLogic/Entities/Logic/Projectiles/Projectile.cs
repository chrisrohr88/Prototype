using UnityEngine;
using System.Collections;
using SF.EventSystem;
using System;

public class Projectile
{
	public Vector3 Velocity { get; private set; }
	public float Speed { get; private set; }
	public GameObject BaseProjectile { get; private set; }

	public DamageData DamageData { get; set; }

	public static Projectile Create(ProjectileSpawnData spawnData, BaseProjectile projectileBase)
    {
		var projectile = new Projectile();
		projectile.Velocity = (spawnData.TargetPosition - spawnData.SpawnTransform.position).normalized;
		projectile.Speed = spawnData.Speed;
		projectileBase.Projectile = projectile;
		projectile.BaseProjectile = projectileBase.gameObject;
        return projectile;
	}

	public void TryToFireEvent()
	{
		try
		{
			SFEventManager.FireEvent(new EnemyHitEventData { OriginId = DamageData.AttackerId, TargetId = DamageData.TargetId, DamageData = DamageData });
		}
		catch(Exception ex)
		{
			Debug.logger.Log("Projectile OnTrigger failed : " + ex.Message);
		}
		GameObject.Destroy(BaseProjectile);
	}
}
