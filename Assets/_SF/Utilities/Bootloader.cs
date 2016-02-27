using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Bootloader : MonoBehaviour
{
	[SerializeField] private string _level = "Prototype"; 

    private void Start()
    {
		Application.targetFrameRate = 60;
        ProfileManager.LoadProfiles(LoadScene);
    }

    private void LoadScene()
    {
		SceneManager.LoadScene(_level);
    }
}
