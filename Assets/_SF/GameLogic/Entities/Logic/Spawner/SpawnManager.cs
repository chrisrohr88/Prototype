using UnityEngine;
using System.Collections.Generic;
using SF.GameLogic.Data.Profiles;

namespace SF.GameLogic.Entities.Logic.Spawner
{
	// TODO: Figure out how to easily remove this
	public static class SpawnManager
	{
		private static List<EnemyProfile> _enemyProfiles = new List<EnemyProfile>();

		public static void LoadLevelEnemies(List<EnemyProfile> enemyProfiles)
	    {
			_enemyProfiles = enemyProfiles;
		}

		public static EnemyProfile GetRandomEnemyProfile()
		{
			int rnd = Random.Range(0, _enemyProfiles.Count);
			return _enemyProfiles[rnd];
		}
	}
}
