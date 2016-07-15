using UnityEngine;
using System.Collections;

namespace SF.GameLogic.Controls.Interactables
{
	public class MovementInteractable : MonoBehaviour, Interactable
	{
		[SerializeField] Transform _controlledObject;

		public Transform ControlledObject
		{
			set
			{
				_controlledObject = value;
			}
		}

		public void OnPress(MyTouch touch)
		{
			_controlledObject.position = touch.WorldHitPosition;
		}

		public void OnRelease(MyTouch touch)
		{
			_controlledObject.position = touch.WorldHitPosition;
		}

		public void OnHold(MyTouch touch)
		{
			_controlledObject.position = touch.WorldHitPosition;
		}

		public void OnMove(MyTouch touch)
		{
			_controlledObject.position = touch.WorldHitPosition;
		}
	}
}
