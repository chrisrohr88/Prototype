using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Bootloader : MonoBehaviour
{
    private void Start()
    {
		Application.targetFrameRate = 60;
        ProfileManager.LoadProfiles(LoadScene);
    }

    private void LoadScene()
    {
		GameManager.Instance.LoadLevel();
    }
}
