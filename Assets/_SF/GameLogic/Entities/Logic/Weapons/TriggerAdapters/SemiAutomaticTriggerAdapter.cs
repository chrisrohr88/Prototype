using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace Weapons.TriggerAdapters
{
	public class SemiAutomaticTriggerAdapter : TriggerAdapter
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
			SFEventManager.FireEvent(new WeaponTriggerEventData { EventType = SFEventType.WeaponTriggerPull, OriginId = _weapon.EntityId, TargetId = _weapon.EntityId, TargetPosition = TargetPosition });				
		}
	}
}
