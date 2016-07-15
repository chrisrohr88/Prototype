using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SF.EventSystem;

public class Spawner
{
	private List<BaseEnemy> _spawnedEnemies;
	private List<SpawnArea> _spawnAreas;
	private EventRegistrar _eventRegistar;
	private bool _isSpawning = false;

	public Spawner()
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
			var enemy = _spawnAreas[0].SpawnEnemy(EnemyManager.GetRandomEnemyProfile());
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

public static class EnemyManager
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
