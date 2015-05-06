using UnityEngine;
using System.Collections;

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

    private static event System.Action _onPressed;
    public static event System.Action OnPressed
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
    
    private static event System.Action _onHeld;
    public static event System.Action OnHeld
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
    
    private static event System.Action _onReleased;
    public static event System.Action OnReleased
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
    
    private static event System.Action _onMoved;
    public static event System.Action OnMoved
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
            _onPressed();
        }
    }

    public void OnRelease(MyTouch touch)
	{
        SetFirePositionFromTouchPosition(touch.WorldHitPosition);
        if(_onReleased != null)
        {
            _onReleased();
        }
    }

    public void OnHold(MyTouch touch)
    {
        SetFirePositionFromTouchPosition(touch.WorldHitPosition);
        if(_onHeld != null)
        {
            _onHeld();
        }
    }

    public void OnMove(MyTouch touch)
    {
        SetFirePositionFromTouchPosition(touch.WorldHitPosition);
        if(_onMoved != null)
        {
            _onMoved();
        }
    }

    public void SetFirePositionFromTouchPosition(Vector3 touchPosition)
    {
        var newFirePosition = _fireTransform.position;
        newFirePosition.y = touchPosition.y;
        _fireTransform.position = newFirePosition;
    }
}
