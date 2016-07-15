using UnityEngine;
using System.Collections.Generic;
using SF.EventSystem;

namespace SF.EventSystem
{
	public abstract class EventRegistrar
	{
		private Dictionary<SFEventType, SFEvent> _registeredEvents;
		private Dictionary<SFEventType, SFEventListener> _registeredEventListeners;

		public EventRegistrar()
		{
			_registeredEvents = new Dictionary<SFEventType, SFEvent>();
			_registeredEventListeners = new Dictionary<SFEventType, SFEventListener>();
			SFEventManager.RegisterEventRegistrar(this);
		}

		public void Unregister()
		{
			UnregisterAllEvents();
			UnregisterEventListners();
		}

		protected void RegisterEvent(SFEventType eventType, long originId = SFEventManager.SYSTEM_ORIGIN_ID)
		{
			var sfEvent = new SFEvent { OriginId = originId, EventType = eventType };
			_registeredEvents.Add(eventType, sfEvent);
			SFEventManager.RegisterEvent(sfEvent);
		}

		protected void RegisterEventListener(SFEventType eventType, SFEventListener eventListner)
		{
			_registeredEventListeners.Add(eventType, eventListner);
			SFEventManager.RegisterEventListener(eventType, eventListner);
		}

		protected void UnregisterAllEvents()
		{
			foreach(var sfEvent in _registeredEvents.Values)
			{
				SFEventManager.UnegisterEvent(sfEvent);
			}
		}

		protected void UnregisterEventListners()
		{
			foreach(var kvp in _registeredEventListeners)
			{
				SFEventManager.UnegisterEventListener(kvp.Key, kvp.Value);
			}
		}

		protected void Register()
		{
			RegisterEvents();
			RegisterEventListeners();
		}


		protected abstract void RegisterEvents();
		protected abstract void RegisterEventListeners();
	}
}
