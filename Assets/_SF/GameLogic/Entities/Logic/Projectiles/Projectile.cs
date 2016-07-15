using UnityEngine;
using System.Collections;
using SF.EventSystem;
using System;
using System.Collections.Generic;

public class Projectile : Entity
{
	public Vector3 Velocity { get; private set; }
	public float Speed { get; private set; }
	public GameObject BaseProjectile { get; private set; }

	public DamageData DamageData { get; set; }
	public GameObject GameObjectHit { get; set; }

	public static Projectile Create(ProjectileSpawnData spawnData, BaseProjectile projectileBase)
    {
		var projectile = new Projectile();
		projectile.Velocity = (spawnData.TargetPosition - spawnData.SpawnTransform.position).normalized;
		projectile.Speed = spawnData.Speed;
		projectileBase.Projectile = projectile;
		projectile.BaseProjectile = projectileBase.gameObject;
		projectile._eventRegistar = new ProjectileEventRegistrar(projectile);
        return projectile;
	}

	private Projectile() : base()
	{
	}

	public void TryToFireEvent()
	{
		try
		{
			SFEventManager.FireEvent(new EntityHitEventData { OriginId = DamageData.AttackerId, TargetId = DamageData.TargetId, DamageData = DamageData });
			SFEventManager.FireEvent(new ProjectileHitEventData { OriginId = EntityId, GameObjectHit = GameObjectHit, HitObjectId = DamageData.TargetId });
			SFEventManager.FireEvent(new SFEventData { OriginId = EntityId, EventType = SFEventType.ProjectileDestroyed });
		}
		catch(Exception ex)
		{
			Debug.logger.Log("Projectile OnTrigger failed : " + ex.Message);
		}
		GameObject.Destroy(BaseProjectile);
	}
}
