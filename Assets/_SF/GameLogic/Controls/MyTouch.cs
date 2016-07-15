using UnityEngine;

namespace SF.GameLogic.Controls
{
	public class MyTouch
	{
	    public TouchPhase TouchPhase { get; set; }
	    public Vector2 Position { get; set; }
	    public Vector2 DeltaPosition { get; set; }
	    public float DeltaTime { get; set; }
	    public int TapCount { get; set; }
	    public object InteractedObject { get; set; }
	    public Vector3 WorldHitPosition { get; set; } 
	    
	    public MyTouch(Touch touch)
	    {
	        TouchPhase = touch.phase;
	        Position = touch.position;
	        DeltaPosition = touch.deltaPosition;
	        DeltaTime = touch.deltaTime;
	        TapCount = touch.tapCount;
	    }
	    
	    public MyTouch()
	    {
	    }
	}
}
