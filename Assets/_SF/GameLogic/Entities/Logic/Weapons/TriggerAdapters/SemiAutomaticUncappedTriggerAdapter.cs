using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace SF.GameLogic.Entities.Logic.Weapons.TriggerAdapters
{
	public class SemiAutomaticUncappedTriggerAdapter : TriggerAdapter
	{
		private const float FIRE_RATE = .175f;
		private float _timeToFire = 0;

		protected override void Fire()
		{
			if(_timeToFire < Time.time)
			{
				_timeToFire = Time.time + FIRE_RATE;
				FireWeaponTrigerEvent(WeaponTriggerEvents.Pulled);
			}
		}
	}
}
