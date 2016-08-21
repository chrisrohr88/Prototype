using System.Collections.Generic;
using SF.EventSystem;
using System.Collections;
using UnityEngine;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.GameLogic.EventSystem.EventRegistrars;
using SF.Utilities.Managers;
using SF.CustomInspector.Attributes;
using SF.CustomInspector.Utilities;

namespace SF.GameLogic.Entities.Logic.Spawner
{
	public class EntitySpawner
	{
		private List<BaseEnemy> _spawnedEnemies;
		[InspectorObject] private List<SpawnArea> _spawnAreas;
		[InspectorValue] private bool _isSpawning = false;
		[InspectorValue] public int _test = 0;

		public EntitySpawner()
		{
			_spawnedEnemies = new List<BaseEnemy>();
			new SpawnerEventRegistrar(this);
			InspectorManager.Add("EntitySpawner", this);
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
			while (true)
			{
				SpawnEntity();
				yield return new WaitForSeconds(5);
			}
		}

		private void SpawnEntity()
		{
			if(_isSpawning)
			{					
				var enemy = _spawnAreas[0].SpawnEnemy(SpawnManager.GetRandomEnemyProfile());
				_spawnedEnemies.Add(enemy);
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
