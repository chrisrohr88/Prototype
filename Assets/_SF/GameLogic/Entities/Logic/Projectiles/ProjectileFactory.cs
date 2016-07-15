using UnityEngine;
using System.Collections;

namespace SF.GameLogic.Projectiles
{
	public class ProjectileSpawnData
	{
		public Transform SpawnTransform { get; set; }
		public Vector3 TargetPosition { get; set; }
		public float Speed { get; set; }
	}

	public static class ProjectileFactory
	{
		public static Projectile CreateProjectileFromProfile(Transform spawnTransform, Vector3 targetPosition) // Add profile
		{
			var spawnData = new ProjectileSpawnData { SpawnTransform = spawnTransform, TargetPosition = targetPosition, Speed = 100 };
			var baseprojectile = (GameObject.Instantiate(Resources.Load("Game/Projectiles/BasicProjectile"), spawnTransform.position, Quaternion.identity) as GameObject).AddComponent<BaseProjectile>();
			return Projectile.Create(spawnData, baseprojectile);
		}
	}
}
