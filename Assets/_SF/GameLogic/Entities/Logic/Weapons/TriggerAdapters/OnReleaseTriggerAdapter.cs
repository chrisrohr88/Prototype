using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace Weapons.TriggerAdapters
{
	public class OnReleaseTriggerAdapter : TriggerAdapter
	{
		private bool _didPullTrigger = false;

		public override void Update()
		{
			if(CurrentState == States.Firing)
			{
				Fire();
			}
			PreviousState = CurrentState;
		}

		private void Fire()
		{
			Debug.Log("Fire");
			if(_didPullTrigger)
			{
				_didPullTrigger = false;
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerRelease, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });
			}
			else
			{
				_didPullTrigger = true;
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerPull, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });								
			}
		}
	}
}
