using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SF.EventSystem;
using SF.Utilities.Managers;

namespace SF.Utilities
{
	public class Bootloader : MonoBehaviour
	{
	    private void Start()
	    {
			Application.targetFrameRate = 60;
	        ProfileManager.LoadProfiles(LoadScene);
			SFEventManager.RegisterGlobalEvents();
	    }

	    private void LoadScene()
	    {
			SceneManager.LoadScene("Main Menu");
	    }
	}
}
