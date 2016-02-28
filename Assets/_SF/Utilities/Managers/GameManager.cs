using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private const string LEVEL_NAME = "Prototype"; 

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

	public SinglePlayerScoreManager ScoreManager { get; private set; }
	public GameMode GameMode { get; private set; }

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
		ScoreManager = new SinglePlayerScoreManager();
		GameMode = new GameMode();
		EnemyManager.LoadEnemies (new List<EnemyProfile> {ProfileManager.GetEnemyProfile("Skeleton")});//, ProfileManager.GetEnemyProfile("Enemy")});
        Application.targetFrameRate = 30;
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene(LEVEL_NAME);
	}
}
