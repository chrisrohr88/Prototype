using UnityEngine;
using System.Collections;

namespace SF.EventSystem
{
	public class SpawnerEventRegistrar : EventRegistrar
	{
		private Spawner _spawner;

		public SpawnerEventRegistrar(Spawner spawner)
		{
			_spawner = spawner;
			Register();
		}

		protected override void RegisterEvents()
		{
			RegisterEvent(SFEventType.EnemySpawned);
		}

		protected override void RegisterEventListeners()
		{
			RegisterEventListener(SFEventType.GameOver, new ConcreteSFEventListener<SFEventData> { MethodToExecute = _spawner.OnGameOver } ); 
			RegisterEventListener(SFEventType.LevelStart, new ConcreteSFEventListener<SFEventData> { MethodToExecute = _spawner.OnLevelStart });
		}
	}
}
