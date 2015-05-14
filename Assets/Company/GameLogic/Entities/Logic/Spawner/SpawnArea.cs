using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EnemyManager
{
    private const string ENEMY_PATH = "Game/Enemies/";
	
	private static List<GameObject> _enemies;
	private static List<EnemyProfile> _enemyProfiles;
    private static List<BaseEnemy> _spawnedEnemies;
    private static List<SpawnArea> _spawnAreas;
    private static bool _isSpawning = false;

    static EnemyManager()
    {
		_enemies = new List<GameObject>();
        _spawnedEnemies = new List<BaseEnemy>();
        _spawnAreas = new List<SpawnArea>();
    }

    public static void EnableSpawning()
    {
        _isSpawning = true;
        GameManager.Instance.StartCoroutine(BeginSpawning());
    }

    private static IEnumerator BeginSpawning()
    {
        SetupSpawnAreas();
        while (_isSpawning)
        {
			int rnd = Random.Range(0, _enemies.Count);
			var enemy = _spawnAreas[0].SpawnEnemy(_enemyProfiles[rnd]);
            _spawnedEnemies.Add(enemy);
            yield return new WaitForSeconds(5);
        }
    }

    public static void DisableSpawning()
    {
        _isSpawning = false;
    }

    private static void SetupSpawnAreas()
    {
        _spawnAreas.Add(new SpawnArea(new Vector3(165, 5, 0)));
    }

    public static void LoadEnemies(List<EnemyProfile> enemyProfiles)
    {
		_enemyProfiles = enemyProfiles;
        _enemies = new List<GameObject>();
		foreach (var profile in enemyProfiles)
        {
			Debug.Log(profile);
            var enemyPrefab = Resources.Load(profile.EnemyPrefabPath) as GameObject;
            _enemies.Add(enemyPrefab);
        }
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
		var enemy = Enemy.Create(enemyProfile);
        enemy.TestEnemy.transform.parent = _enemyParentObject.transform;
		enemy.TestEnemy.transform.localPosition = _spawnAreaCenter + new Vector3(0, Random.Range(-_maxDistance, _maxDistance), -50);
        return enemy.TestEnemy;
    }
}
