using UnityEngine;
using System.Collections;
using SF.EventSystem;
using SF.GameLogic.EventSystem.EventData;

namespace SF.GameLogic.Entities.Logic.Weapons.TriggerAdapters
{
	public abstract class TriggerAdapter
	{

		public enum States
		{
			Firing,
			Waiting
		}

		protected enum WeaponTriggerEvents
		{
			Pulled,
			Held,
			Released
		}

		public States CurrentState { get; set; }
		public States PreviousState { get; set; }
		protected Weapon _weapon;
		public Vector3 TargetPosition { get; set; }

		public static TriggerAdapter Create(Weapon weapon)
		{
			TriggerAdapter triggerAdapter;
			triggerAdapter = new OnReleaseTriggerAdapter();
			triggerAdapter.CurrentState = triggerAdapter.PreviousState = States.Waiting;
			triggerAdapter._weapon = weapon;
			return triggerAdapter;
		}

		public void StartFiring()
		{
			if(CurrentState == States.Waiting)
			{
				CurrentState = States.Firing;
			}
		}

		public void StopFiring()
		{
			if(CurrentState == States.Firing)
			{
				CurrentState = States.Waiting;
				FireWeaponTrigerEvent(WeaponTriggerEvents.Released);
			}
		}

		public void Update()
		{
			if(CurrentState == States.Firing)
			{
				Fire();
			}
			PreviousState = CurrentState;
		}

		protected void FireWeaponTrigerEvent(WeaponTriggerEvents eventType)
		{
			switch(eventType)
			{
			case WeaponTriggerEvents.Held:
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerHold, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });
				break;
			case WeaponTriggerEvents.Pulled:
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerPull, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });
				break;
			case WeaponTriggerEvents.Released:
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerRelease, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });				
				break;
			}
		}

		protected abstract void Fire();
	}
}
