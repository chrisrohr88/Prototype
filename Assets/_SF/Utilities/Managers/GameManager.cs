using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using SF.EventSystem;

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
        Application.targetFrameRate = 30;
	}

	public void SetGameMode(GameModeType gameModeType)
	{
		switch(gameModeType)
		{
			case GameModeType.Survival:
				GameMode = new GameMode();
				break;
		}
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene(LEVEL_NAME);
	}

	public void StartGame()
	{
		if(GameMode == null)
		{
			SetGameMode(GameModeType.Survival);
		}
		GameMode.StartGame();
	}

	public void EndGame()
	{
		SFEventManager.FireEvent(new SFEventData { OriginId = SFEventManager.SYSTEM_ORIGIN_ID, EventType = SFEventType.GameOver });
	}
}
