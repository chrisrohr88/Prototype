using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
	public void OnButtonPressed()
	{
		GameManager.Instance.LoadLevel();
	}
}
