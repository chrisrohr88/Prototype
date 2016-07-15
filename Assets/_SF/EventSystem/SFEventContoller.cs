using UnityEngine;
using System.Collections.Generic;
using System;

namespace SF.EventSystem
{
	public class SFEventContoller
	{		
		private Dictionary<long, SFEventListener> _targetedListner = new Dictionary<long, SFEventListener>();
		private List<SFEventListener> _globalEventListners = new List<SFEventListener>();
		private Dictionary<long, SFEvent> _events = new Dictionary<long, SFEvent>();

		public void FireEvent<T>(T eventData) where T : SFEventData
		{
			if(!_events.ContainsKey(eventData.OriginId))
			{
				Debug.LogWarning(string.Format("EventType {0} for ObjectId {1} has not been registered.", eventData.EventType, eventData.OriginId));
				return;
			}

			if(eventData.TargetId.HasValue)
			{
				try
				{					
					_targetedListner[eventData.TargetId.Value].EventHandlerMethod(eventData);
				}
				catch
				{
					Debug.LogWarning(string.Format("EventType {0} for ObjectId {1} does not have a registered listner for TargetId {2}.", eventData.EventType, eventData.OriginId, eventData.TargetId.Value));
				}
			}

			foreach(var eventListner in _globalEventListners)
			{
				eventListner.EventHandlerMethod(eventData);
			}

			#if DEBUGs
			Debug.logger.Log(eventData.EventType + " was fired.");
			#endif
		}

		public void RegisterEvent(SFEvent eventToRegister)
		{
			try
			{
				_events.Add(eventToRegister.OriginId, eventToRegister);
			}
			catch(Exception ex)
			{
				Debug.LogWarning("Could not register event for EventType " + eventToRegister.EventType + " : " + ex.Message);
			}
		}

		public void UnregisterEvent(SFEvent eventToRegister)
		{
			if(_events.ContainsKey(eventToRegister.OriginId))
			{
				_events.Remove(eventToRegister.OriginId);
			}
		}

		public void RegisterEventListener(SFEventType eventType, SFEventListener eventListner)
		{
			try
			{
				if(eventListner.TargetId.HasValue)
				{
					_targetedListner.Add(eventListner.TargetId.Value, eventListner);
				}
				else
				{
					_globalEventListners.Add(eventListner);
				}
			}
			catch(Exception ex)
			{
				Debug.LogWarning("Could not register listner for EventType " + eventType + " : " + ex.Message);
			}
		}

		public void UnregisterEventListener(SFEventType eventType, SFEventListener eventListner)
		{
			if(eventListner.TargetId.HasValue)
			{
				try
				{
					_targetedListner.Remove(eventListner.TargetId.Value);
				}
				catch
				{
					Debug.LogWarning(string.Format("EventType {0} does not have a registered listner for TargetId {1}.", eventType, eventListner.TargetId.Value));
				}
			}
			else
			{
				_globalEventListners.Remove(eventListner);
			}
		}
	}
}
