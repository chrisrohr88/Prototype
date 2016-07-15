using UnityEngine;
using System.Collections;

namespace SF.GameLogic.Controls.Interactables
{
	public class FieldInteractable : MonoBehaviour, Interactable
	{
	    [SerializeField] private Transform _fireTransform;

	    public Transform FireTransform
	    {
	        get
	        {
	            return _fireTransform;
	        }
	    }

	    private static event System.Action<Vector3> _onPressed;
		public static event System.Action<Vector3> OnPressed
	    {
	        add
	        {
	            _onPressed += value;
	        }
	        remove
	        {
	            _onPressed -= value;
	        }
	    }
	    
		private static event System.Action<Vector3> _onHeld;
		public static event System.Action<Vector3> OnHeld
	    {
	        add
	        {
	            _onHeld += value;
	        }
	        remove
	        {
	            _onHeld -= value;
	        }
	    }
	    
		private static event System.Action<Vector3> _onReleased;
		public static event System.Action<Vector3> OnReleased
	    {
	        add
	        {
	            _onReleased += value;
	        }
	        remove
	        {
	            _onReleased -= value;
	        }
	    }
	    
		private static event System.Action<Vector3> _onMoved;
		public static event System.Action<Vector3> OnMoved
	    {
	        add
	        {
	            _onMoved += value;
	        }
	        remove
	        {
	            _onMoved -= value;
	        }
	    }

		public void Start()
		{
			_fireTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 100));
		}

	    public void OnPress(MyTouch touch)
	    {
	        SetFirePositionFromTouchPosition(touch.WorldHitPosition);
	        if(_onPressed != null)
	        {
	            _onPressed(touch.WorldHitPosition);
	        }
	    }

	    public void OnRelease(MyTouch touch)
		{
	        SetFirePositionFromTouchPosition(touch.WorldHitPosition);
	        if(_onReleased != null)
	        {
				_onReleased(touch.WorldHitPosition);
	        }
	    }

	    public void OnHold(MyTouch touch)
	    {
	        SetFirePositionFromTouchPosition(touch.WorldHitPosition);
	        if(_onHeld != null)
	        {
				_onHeld(touch.WorldHitPosition);
	        }
	    }

	    public void OnMove(MyTouch touch)
	    {
	        SetFirePositionFromTouchPosition(touch.WorldHitPosition);
	        if(_onMoved != null)
	        {
				_onMoved(touch.WorldHitPosition);
	        }
	    }

	    public void SetFirePositionFromTouchPosition(Vector3 touchPosition)
	    {
	        var newFirePosition = _fireTransform.position;
	        newFirePosition.y = touchPosition.y;
	        _fireTransform.position = newFirePosition;
	    }
	}
}
