﻿using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _secondsToLive = 5;

    private void Awake()
    {
        Destroy(gameObject, _secondsToLive);
    }
}
