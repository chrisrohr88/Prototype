using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SF.GameLogic.Data.Enums;
using SF.Utilities.Managers;

namespace SF.GameLogic.GameModes
{
	//TODO: Do this better
	public class LevelLoader : MonoBehaviour
	{
		private void Start()
		{
			GameManager.Instance.SetGameMode(GameModeType.Survival);
			GameManager.Instance.StartGame();
		}
	}
}
