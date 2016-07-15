using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace Weapons.TriggerAdapters
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
