using UnityEngine;
using System.Collections;
using SF.EventSystem;
using SF.Utilities.Extensions;

namespace SF.Utilities
{
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
}
