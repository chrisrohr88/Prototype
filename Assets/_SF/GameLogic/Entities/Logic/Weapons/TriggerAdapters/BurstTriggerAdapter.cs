using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace Weapons.TriggerAdapters
{
	public class BurstTriggerAdapter : TriggerAdapter
	{
		protected override void Fire()
		{
			FireWeaponTrigerEvent(WeaponTriggerEvents.Pulled);
		}
	}
}
