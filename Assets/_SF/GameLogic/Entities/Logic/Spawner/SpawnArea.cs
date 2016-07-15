using UnityEngine;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.EventSystem;
using SF.GameLogic.Data.Profiles;
using SF.GameLogic.EventSystem.EventData;

namespace SF.GameLogic.Entities.Logic.Spawner
{
	public class SpawnArea
	{
	    private GameObject _enemyParentObject;
	    private float _maxDistance = 50;
	    private Vector3 _spawnAreaCenter;

	    public SpawnArea(Vector3 spawnAreaCenter)
	    {
	        _enemyParentObject = GameObject.Find("Enemies"); // TODO fix to have reference
	        _spawnAreaCenter = spawnAreaCenter;
	    }

		public BaseEnemy SpawnEnemy(EnemyProfile enemyProfile)
	    {
			var enemy = CharacterFactory.CreateEnemyFromProfile(enemyProfile);
			enemy.EnemyRenderable.transform.parent = _enemyParentObject.transform;
			enemy.EnemyRenderable.transform.localPosition = _spawnAreaCenter + new Vector3(0, Random.Range(-_maxDistance, _maxDistance), -50);
			SFEventManager.FireEvent(new EnemySpawnEventData { EnemyId = enemy.EntityId });
			return enemy.EnemyRenderable;
	    }
	}
}
