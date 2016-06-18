namespace SF.EventSystem
{
	public enum SFEventType
	{
		// Global Events
		LevelEnded = 0,
		LevelStart = 1,

		// Enemy Events
		EnemySpawned = 1000,
		EnemyDeath = 1001,
		EnemyAttack = 1002,
		EnemyHit = 1003
	}
}
