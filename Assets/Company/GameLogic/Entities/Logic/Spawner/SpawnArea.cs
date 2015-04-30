using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EnemyManager
{
    private const string ENEMY_PATH = "Game/Enemies/";
    
    private static List<GameObject> _enemies;
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
            var enemy = _spawnAreas[0].SpawnEnemy(_enemies[rnd]).GetComponent<BaseEnemy>();
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
        _spawnAreas.Add(new SpawnArea(new Vector3(0, 80, 0)));
    }

    public static void LoadEnemies(List<string> enemyNames)
    {
        _enemies = new List<GameObject>();
        foreach (var enemyName in enemyNames)
        {
            var enemyPrefab = Resources.Load(ENEMY_PATH + enemyName) as GameObject;
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
        _enemyParentObject = GameObject.Find("Enemies");
        _spawnAreaCenter = spawnAreaCenter;
    }

    public GameObject SpawnEnemy(GameObject enemyToSpawn)
    {
        var enemy = GameObject.Instantiate(enemyToSpawn) as GameObject;
        enemy.transform.parent = _enemyParentObject.transform;
        enemy.transform.localPosition = _spawnAreaCenter + new Vector3(Random.Range(-_maxDistance, _maxDistance), 0, -50);
        return enemy;
    }
}
