using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class InputStrategy : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    protected List<MyTouch> _touches;

    protected abstract void CheckForTouches();
    protected abstract void EndInputUpdate();

    private void Update()
    {
        ResetTouches();
        CheckForTouches();
        HandleTouches();
        EndInputUpdate();
    }

    private void ResetTouches()
    {
        _touches = new List<MyTouch>();
    }

    private void HandleTouches()
    {
        foreach (var touch in _touches)
        {
            HandleTouch(touch);
        }
    }

    protected MyTouch CreateTouchWithTouchPhase(TouchPhase touchPhase, Vector2 lastTouchPosition)
    {
		var touch = new MyTouch();
		touch.TouchPhase = touchPhase;
        touch.Position = Input.mousePosition;
        touch.DeltaPosition = touch.Position - lastTouchPosition;
        touch.DeltaTime = Time.deltaTime;
        touch.TapCount = 1;
        _touches.Add(touch);
        return touch;
    }

    private void HandleTouch(MyTouch touch)
    {
        var origin = _camera.ScreenToWorldPoint(touch.Position);
        var rayCastHit = GetFirstRaycastHit(origin);
        if(rayCastHit.transform != null)
        {
            touch.WorldHitPosition = rayCastHit.point;
            var interactableObject = GetInteractableFromRayCastHit(rayCastHit);
            SendTouchToInteractableObject(touch, interactableObject);
        }
        else
        {
            Debug.Log("Hit is null");
        }
    }
    private RaycastHit2D GetFirstRaycastHit(Vector3 origin)
    {
		var ray = new Ray(origin, new Vector3(0, 0, 1000));
        var layerMask = 1 << LayerMask.NameToLayer("Interactable");
        return Physics2D.GetRayIntersection(ray, 1000, layerMask);
    }

    private Interactable GetInteractableFromRayCastHit(RaycastHit2D rayCastHit)
    {
        return rayCastHit.transform.gameObject.GetComponentIncludingInterface<Interactable>();
    }

    private void SendTouchToInteractableObject(MyTouch touch, Interactable interactableObject)
    {
        if(interactableObject != null)
        {
            if (touch.TouchPhase == TouchPhase.Began)
            {
                interactableObject.OnPress(touch);
            }

            if (touch.TouchPhase == TouchPhase.Ended)
            {
                interactableObject.OnRelease(touch);
            }
        
            if (touch.TouchPhase == TouchPhase.Stationary)
            {
                interactableObject.OnHold(touch);
            }
        
            if (touch.TouchPhase == TouchPhase.Moved)
            {
                interactableObject.OnMove(touch);
            }
        }
    }
}
