namespace SF.EventSystem
{
	public enum SFEventType
	{
		// Global Events 0000 - 0999
		LevelEnded = 0,
		LevelStart = 1,
		GameOver = 2,
		SinglePlayerScoreUpdate = 3,

		// Enemy Events 1000 - 1999
		EnemySpawned = 1000,
		EnemyDeath = 1001,

		// Player Events 3000 - 3999
		PlayerDeath = 3000,

		// Weapon Events 4000 - 4999
		WeaponFired = 4000,
		WeaponTriggerPull = 4001,
		WeaponTriggerHold = 4002,
		WeaponTriggerRelease = 4003,
		WeaponReloaded = 4004,

		// Projectile Events 5000 - 5999
		ProjectileHit = 5000,
		ProjectileDestroyed = 5001,


		// Entity Events
		EntityAttack = 8000,
		EntityHit = 8001,
	}
}
