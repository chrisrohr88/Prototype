using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace SF.GameLogic.Entities.Logic.Weapons.TriggerAdapters
{
	public class ChargedTriggerAdapter : TriggerAdapter
	{
		private bool _didPullTrigger = false;
		private float _timeToFire = 0;

		protected override void Fire()
		{
			if(_didPullTrigger && _timeToFire < Time.time)
			{
				_didPullTrigger = false;
				FireWeaponTrigerEvent(WeaponTriggerEvents.Released);
			}
			else
			{
				_didPullTrigger = true;
				_timeToFire = Time.time + _weapon.ChargeTime.ModifiedValue;
				FireWeaponTrigerEvent(WeaponTriggerEvents.Pulled);
			}
		}
	}
}