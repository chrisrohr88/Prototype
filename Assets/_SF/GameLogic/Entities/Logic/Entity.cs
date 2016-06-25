using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FlakeGen;
using SF.EventSystem;

public enum GeneratorIds
{
	Entity = 0
}

public class EventRegistar
{
	private List<SFEvent> _eventsToRegister;

	private Dictionary<SFEventType, SFEvent> _registeredEvents;

	public EventRegistar(List<SFEvent> eventsToRegister)
	{
		_eventsToRegister = eventsToRegister;
		_registeredEvents = new Dictionary<SFEventType, SFEvent>();
	}

	public void RegisterEvents()
	{
		foreach(var eventToRegister in _eventsToRegister)
		{
			SFEventManager.RegisterEvent(eventToRegister);
			_registeredEvents.Add(eventToRegister.EventType, eventToRegister);
		}
	}

	public void UnregisterAllEvents()
	{
		Debug.Log("Unregister all events");
		foreach(var sfEvent in _registeredEvents.Values)
		{
			SFEventManager.UnegisterEvent(sfEvent);
		}
	}

	public void RegisterEventListners()
	{
	}

	public void UnregisterEventListners()
	{
	}
}

public class Entity
{
	private static Id64Generator ID_GENERATOR = new Id64Generator((int)GeneratorIds.Entity);

	protected EventRegistar _eventRegistar;

	public long EntityId { get; private set; }

	public Entity()
	{
		EntityId = ID_GENERATOR.GenerateId();
		Debug.Log(EntityId);
	}

	protected virtual void OnDeath()
	{
		_eventRegistar.UnregisterAllEvents();
	}
}
