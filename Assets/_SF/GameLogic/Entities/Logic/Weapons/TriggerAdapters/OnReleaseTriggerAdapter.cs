using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace Weapons.TriggerAdapters
{
	public class OnReleaseTriggerAdapter : TriggerAdapter
	{
		private bool _didPullTrigger = false;

		protected override void Fire()
		{
			if(_didPullTrigger)
			{
				_didPullTrigger = false;
				FireWeaponTrigerEvent(WeaponTriggerEvents.Released);
			}
			else
			{
				_didPullTrigger = true;
				FireWeaponTrigerEvent(WeaponTriggerEvents.Pulled);
			}
		}
	}
}
