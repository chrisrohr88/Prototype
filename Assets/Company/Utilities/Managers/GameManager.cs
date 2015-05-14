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
		if(_instance == null)
		{
			_instance = this;
		}
		else if(_instance != this)
		{
			Destroy(gameObject);
			return;
		}

		EnemyManager.LoadEnemies (new List<EnemyProfile> {ProfileManager.GetEnemyProfile("Skeleton")});//, ProfileManager.GetEnemyProfile("Enemy")});
        Application.targetFrameRate = 30;
	}
}
