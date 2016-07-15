using UnityEngine;
using System.Collections;

namespace SF.Utilities
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
	    private void Awake()
	    {
	        DontDestroyOnLoad(this.gameObject);
	    }
	}
}
