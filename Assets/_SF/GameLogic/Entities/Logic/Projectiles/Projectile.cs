using UnityEngine;
using System.Collections;

public class Projectile
{
	public Vector3 Velocity { get; private set; }
	public float Speed { get; private set; }
	public GameObject BaseProjectile { get; private set; }

	public static Projectile Create(ProjectileSpawnData spawnData, BaseProjectile projectileBase)
    {
		var projectile = new Projectile();
		projectile.Velocity = (spawnData.TargetPosition - spawnData.SpawnTransform.position).normalized;
		projectile.Speed = spawnData.Speed;
		projectileBase.Projectile = projectile;
		projectile.BaseProjectile = projectileBase.gameObject;
        return projectile;
	}
}
