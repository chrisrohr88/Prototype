using UnityEngine;
using System.Collections;
using SF.GameLogic.Entities.Logic.Spawner;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventRegistrars
{
	public class SpawnerEventRegistrar : EventRegistrar
	{
		private EntitySpawner _spawner;

		public SpawnerEventRegistrar(EntitySpawner spawner)
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
