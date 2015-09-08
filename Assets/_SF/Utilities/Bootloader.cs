using UnityEngine;
using System.Collections;

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
		Application.LoadLevel(_level);
    }
}
