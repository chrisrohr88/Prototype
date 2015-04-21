using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                var go = new GameObject();
				go.name = "GameManager";
                _instance = go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

	private void Awake ()
    {
        EnemyManager.LoadEnemies(new List<string> {"Enemy"});
        Application.targetFrameRate = 30;
	}
}
