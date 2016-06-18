using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseInputStrategy : InputStrategy
{
    private Vector2 _lastMousePosition;
    private bool _mouseIsDown = false;
    
    protected override void CheckForTouches()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }            
        else if(_mouseIsDown)
        {
            HandleMouseWasDown();
        }
    }
    
    protected override void EndInputUpdate()
    {
        _lastMousePosition = Input.mousePosition;
    }
    
    protected void HandleMouseUp()
    {
        CreateTouchWithTouchPhase(TouchPhase.Ended, _lastMousePosition);
        _mouseIsDown = false;
    }
    
    protected void HandleMouseDown()
	{
        CreateTouchWithTouchPhase(TouchPhase.Began, _lastMousePosition);
        _mouseIsDown = true;
    }
    
    protected void HandleMouseWasDown()
    {
        var touch = CreateTouchWithTouchPhase(TouchPhase.Stationary, _lastMousePosition);
        if(touch.DeltaPosition.sqrMagnitude > 1f)
        {
            touch.TouchPhase = TouchPhase.Moved;
        }
        else
        {
            touch.TouchPhase = TouchPhase.Stationary;
        }
    }
}
