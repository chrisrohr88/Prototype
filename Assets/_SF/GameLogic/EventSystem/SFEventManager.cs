using UnityEngine;
using System.Collections.Generic;
using System;

namespace SF.EventSystem
{
	public static class SFEventManager
	{
		public static long SYSTEM_ORIGIN_ID = -7017234;

		private static Dictionary<SFEventType, SFEventContoller> _events = new Dictionary<SFEventType, SFEventContoller>();

		public static void Initialize()
		{
			RegisterGlobalEvents();
		}

		private static void RegisterGlobalEvents()
		{
			RegisterEvent(new SFEvent { OriginId = SYSTEM_ORIGIN_ID, EventType = SFEventType.LevelStart });
		}

		public static void FireEvent<T>(T eventData) where T : SFEventData
		{
			try
			{
				_events[eventData.EventType].FireEvent(eventData);
			}
			catch(Exception ex)
			{
				Debug.logger.Log(ex.Message);
				Debug.Log(string.Format("EventType {0} has not been registered.", eventData.EventType));
			}
		}

		public static void RegisterEvent(SFEvent eventToRegister)
		{
			if(_events.ContainsKey(eventToRegister.EventType))
			{
				var controller = _events[eventToRegister.EventType];
				controller.RegisterEvent(eventToRegister);
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
			Debug.logger.Log("Unregistering event " + eventToUnregister.EventType);
			if(_events.ContainsKey(eventToUnregister.EventType))
			{
				_events[eventToUnregister.EventType].UnregisterEvent(eventToUnregister);
			}
		}

		public static void RegisterEventListner(SFEventType eventType, SFEventListner eventListner)
		{
			if(_events.ContainsKey(eventType))
			{
				_events[eventType].RegisterEventListner(eventType, eventListner);
			}
			else
			{
				var eventController = new SFEventContoller();
				_events.Add(eventType, eventController);
				eventController.RegisterEventListner(eventType, eventListner);
			}
		}

		public static void UnegisterEventListner(SFEventType eventType, SFEventListner eventListner)
		{
			if(_events.ContainsKey(eventType))
			{
				_events[eventType].UnregisterEventListner(eventType, eventListner);
			}
		}
	}
}
