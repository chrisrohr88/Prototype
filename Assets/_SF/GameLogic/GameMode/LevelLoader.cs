using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


//TODO Do this better
public class LevelLoader : MonoBehaviour
{
	private void Start()
	{
		GameManager.Instance.SetGameMode(GameModeType.Survival);
		GameManager.Instance.GameMode.StartGame();
	}
}
