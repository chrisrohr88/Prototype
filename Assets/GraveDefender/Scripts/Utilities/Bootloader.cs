﻿using UnityEngine;
using System.Collections;

public class Bootloader : MonoBehaviour
{
    private void Start()
    {
		Application.targetFrameRate = 60;
        ProfileManager.LoadProfiles(LoadScene);
    }

    private void LoadScene()
    {
        Application.LoadLevel("Prototype");
    }
}
