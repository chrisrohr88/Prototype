using UnityEngine;
using System.Collections.Generic;

namespace SF.EventSystem
{
	public class SFEventContoller
	{		
		private Dictionary<long, SFEventListner> _targetedListner = new Dictionary<long, SFEventListner>();
		private List<SFEventListner> _globalEventListners = new List<SFEventListner>();
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

			#if DEBUG
			Debug.logger.Log(eventData.EventType + " was fired.");
			#endif
		}

		public void RegisterEvent(SFEvent eventToRegister)
		{
			_events.Add(eventToRegister.OriginId, eventToRegister);
		}

		public void UnregisterEvent(SFEvent eventToRegister)
		{
			if(_events.ContainsKey(eventToRegister.OriginId))
			{
				_events.Remove(eventToRegister.OriginId);
			}
		}

		public void RegisterEventListner(SFEventType eventType, SFEventListner eventListner)
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

		public void UnregisterEventListner(SFEventType eventType, SFEventListner eventListner)
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
