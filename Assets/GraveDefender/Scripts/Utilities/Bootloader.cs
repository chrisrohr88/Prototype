using UnityEngine;
using System.Collections;

public class Bootloader : MonoBehaviour
{
	private void Start ()
    {
        Application.LoadLevel("Prototype");
	}
}
