using UnityEngine;
using System.Collections.Generic;
using System;

namespace SF.EventSystem
{
	public static class SFEventManager
	{
		public const long SYSTEM_ORIGIN_ID = -7017234;

		private static Dictionary<SFEventType, SFEventContoller> _events = new Dictionary<SFEventType, SFEventContoller>();
		private static List<EventRegistrar> _eventRegistrars = new List<EventRegistrar>();

		public static void RegisterGlobalEvents()
		{
			RegisterEvent(new SFEvent { OriginId = SYSTEM_ORIGIN_ID, EventType = SFEventType.LevelStart });
			RegisterEvent(new SFEvent { OriginId = SYSTEM_ORIGIN_ID, EventType = SFEventType.GameOver });
		}

		public static void FireEvent<T>(T eventData) where T : SFEventData
		{
			try
			{
				_events[eventData.EventType].FireEvent(eventData);

				if(eventData.EventType == SFEventType.GameOver)
				{
					RegisterAllEventRegistrars();
				}
			}
			catch(Exception ex)
			{
				Debug.logger.Log(ex.Message);
				Debug.LogWarning(string.Format("EventType {0} has not been registered.", eventData.EventType));
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

		public static void RegisterEventListener(SFEventType eventType, SFEventListener eventListner)
		{
			if(_events.ContainsKey(eventType))
			{
				_events[eventType].RegisterEventListener(eventType, eventListner);
			}
			else
			{
				var eventController = new SFEventContoller();
				_events.Add(eventType, eventController);
				eventController.RegisterEventListener(eventType, eventListner);
			}
		}

		public static void UnegisterEventListener(SFEventType eventType, SFEventListener eventListner)
		{
			if(_events.ContainsKey(eventType))
			{
				_events[eventType].UnregisterEventListener(eventType, eventListner);
			}
		}

		public static void RegisterEventRegistrar(EventRegistrar eventRegistrar)
		{
			_eventRegistrars.Add(eventRegistrar);
		}

		private static void RegisterAllEventRegistrars()
		{
			foreach(var eventRegistrar in _eventRegistrars)
			{
				eventRegistrar.Unregister();
			}
			_eventRegistrars.Clear();
		}
	}
}
