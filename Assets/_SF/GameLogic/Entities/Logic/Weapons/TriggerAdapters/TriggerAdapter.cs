using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace Weapons.TriggerAdapters
{
	public abstract class TriggerAdapter
	{

		public enum States
		{
			Firing,
			Waiting
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
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerRelease, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });				
			}
		}

		public abstract void Update();
	}
}


//None,
//Automatic,
//SemiAuto,
//SemiAutoUnCapped,
//OnRelease,
//Charged,
//WarmFirst,
//Burst,
//Spread,
//AI