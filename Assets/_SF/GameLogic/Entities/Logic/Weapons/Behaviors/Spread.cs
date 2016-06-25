using UnityEngine;
using Weapons.Internal;
using Weapons.Behaviors.Enums;

namespace Weapons.Behaviors
{
	public class Spread : WeaponBehavior
	{
		public override void PerformAction()
		{
			// Do nothing, may use for animation
		}
		
		public override void OnTriggerPressed()
		{
			Enabled = true;
			SpreadUse();
		}
		
		public override void OnTriggerRelease()
		{
			if(_usageType == WeaponBehaviorUsageType.Actor)
			{
				SpreadUse();
			}
		}
		
		public override void OnTriggerHeld()
		{
			if(_usageType == WeaponBehaviorUsageType.Actor)
			{
				SpreadUse();
			}
		}
		
		private void SpreadUse()
		{
			if(Weapon.CanUse() && Enabled)
			{
				for(int i = 0; i < Weapon.Weapon.BurstCount.ModifiedValue; i++)
				{
					Weapon.ResetNextTimeToUse();
					Use();
				}
				Enabled = false;
			}
		}
	}
}