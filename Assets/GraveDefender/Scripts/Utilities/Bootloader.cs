using UnityEngine;
using System.Collections;

public class Bootloader : MonoBehaviour
{
    private void Start()
    {
        ProfileManager.LoadProfiles(LoadScene);
    }

    private void LoadScene()
    {
        Application.LoadLevel("Prototype");
    }
}
