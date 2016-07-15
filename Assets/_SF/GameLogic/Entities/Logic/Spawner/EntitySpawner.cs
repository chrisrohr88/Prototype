using System.Collections.Generic;
using SF.EventSystem;
using System.Collections;
using UnityEngine;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.GameLogic.EventSystem.EventRegistrars;
using SF.Utilities.Managers;

namespace SF.GameLogic.Entities.Logic.Spawner
{
	public class EntitySpawner
	{
		private List<BaseEnemy> _spawnedEnemies;
		private List<SpawnArea> _spawnAreas;
		private EventRegistrar _eventRegistar;
		private bool _isSpawning = false;

		public EntitySpawner()
		{
			_spawnedEnemies = new List<BaseEnemy>();
			_eventRegistar = new SpawnerEventRegistrar(this);
		}

		public void OnLevelStart(SFEventData eventData)
		{
			EnableSpawning();
		}

		public void OnGameOver(SFEventData eventData)
		{
			DisableSpawning();
		}

		public void EnableSpawning()
		{
			_spawnAreas = new List<SpawnArea>();
			SetupSpawnAreas();
			_isSpawning = true;
			GameManager.Instance.StartCoroutine(BeginSpawning());
		}

		private IEnumerator BeginSpawning()
		{
			while (_isSpawning)
			{
				var enemy = _spawnAreas[0].SpawnEnemy(SpawnManager.GetRandomEnemyProfile());
				_spawnedEnemies.Add(enemy);
				yield return new WaitForSeconds(5);
			}
		}

		public void DisableSpawning()
		{
			_isSpawning = false;
		}

		private void SetupSpawnAreas()
		{
			_spawnAreas.Add(new SpawnArea(new Vector3(165, 5, 0)));
		}
	}
}
