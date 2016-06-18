using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using SF.EventSystem;

public class Bootloader : MonoBehaviour
{
    private void Start()
    {
		Application.targetFrameRate = 60;
        ProfileManager.LoadProfiles(LoadScene);
		SFEventManager.Initialize();
    }

    private void LoadScene()
    {
		SceneManager.LoadScene("Main Menu");
    }
}
