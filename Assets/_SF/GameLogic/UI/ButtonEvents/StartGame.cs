using UnityEngine;
using System.Collections;
using SF.Utilities.Managers;

namespace SF.GameLogic.UI.ButtonEvents
{
	public class StartGame : MonoBehaviour
	{
		public void OnButtonPressed()
		{
			GameManager.Instance.LoadLevel();
		}
	}
}
