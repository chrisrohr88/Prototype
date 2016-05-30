using UnityEngine;
using System.Collections.Generic;

namespace SF.EventSystem
{
	public static class SFEventManager
	{
		public static long GLOBAL_EVENT_ID = -7017234;

		private static Dictionary<SFEventType, SFEventContoller> _events;

		public static void Initialize()
		{
			_events = new Dictionary<SFEventType, SFEventContoller>();
			RegisterGlobalEvents();
		}

		private static void RegisterGlobalEvents()
		{
			RegisterEvent(new SFEvent { OriginId = GLOBAL_EVENT_ID, EventType = SFEventType.LevelStart });
		}

		public static void FireEvent(SFEventData eventData)
		{
			try
			{
				_events[eventData.EventType].FireEvent(eventData);
			}
			catch
			{
				Debug.Log(string.Format("EventType {0} has not been registered.", eventData.EventType));
			}
		}

		public static void RegisterEvent(SFEvent eventToRegister)
		{
			if(_events.ContainsKey(eventToRegister.EventType))
			{
				_events[eventToRegister.EventType].RegisterEvent(eventToRegister);
			}
			else
			{
				var eventController = new SFEventContoller();
				_events.Add(eventToRegister.EventType, eventController);
				eventController.RegisterEvent(eventToRegister);
			}
		}

		public static void UnegisterEvent(SFEvent eventToUnregister)
		{
			if(_events.ContainsKey(eventToUnregister.EventType))
			{
				_events[eventToUnregister.EventType].UnregisterEvent(eventToUnregister);
			}
		}

		public static void RegisterEventListner(SFEventType eventType, SFEventListner eventListner)
		{
			if(_events.ContainsKey(eventType))
			{
				_events[eventType].RegisterEventListner(eventListner);
			}
			else
			{
				var eventController = new SFEventContoller();
				_events.Add(eventType, eventController);
				eventController.RegisterEventListner(eventListner);
			}
		}

		public static void UnegisterEvent(SFEventType eventType, SFEventListner eventListner)
		{
			if(_events.ContainsKey(eventType))
			{
				_events[eventType].UnregisterEventListner(eventType, eventListner);
			}
		}
	}
}
