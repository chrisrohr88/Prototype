using UnityEngine;
using System.Collections;
using SF.EventSystem;

public class DummyGameObject : MonoBehaviour 
{
	private event System.Action _onUpdate;
	public event System.Action OnUpdate
	{
		add
		{
			_onUpdate += value;
		}
		remove
		{
			_onUpdate -= value;
		}
	}

	private void Update()
	{
		_onUpdate.SafeInvoke();
	}
}
