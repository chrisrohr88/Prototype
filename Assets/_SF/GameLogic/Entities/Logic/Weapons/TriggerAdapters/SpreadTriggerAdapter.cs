using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace SF.GameLogic.Entities.Logic.Weapons.TriggerAdapters
{
	public class SpreadTriggerAdapter : TriggerAdapter
	{
		protected override void Fire()
		{
			FireWeaponTrigerEvent(WeaponTriggerEvents.Pulled);
		}
	}
}
