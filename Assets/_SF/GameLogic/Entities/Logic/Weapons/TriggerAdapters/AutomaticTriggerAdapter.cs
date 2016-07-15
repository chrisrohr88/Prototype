using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace SF.GameLogic.Entities.Logic.Weapons.TriggerAdapters
{
	public class AutomaticTriggerAdapter : TriggerAdapter
	{
		protected override void Fire()
		{
			if(PreviousState == States.Waiting)
			{
				FireWeaponTrigerEvent(WeaponTriggerEvents.Pulled);
			}
			else
			{
				FireWeaponTrigerEvent(WeaponTriggerEvents.Held);
			}
		}
	}
}
