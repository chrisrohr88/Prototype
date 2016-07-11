using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace Weapons.TriggerAdapters
{
	public class AutomaticTriggerAdapter : TriggerAdapter
	{
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
			if(PreviousState == States.Waiting)
			{
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerPull, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });				
			}
			else
			{
				SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerHold, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });
			}
		}
	}
}
