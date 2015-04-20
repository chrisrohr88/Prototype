using UnityEngine;
using Weapons.Internal;

namespace Weapons.Behaviors
{
	public class WarmBeforeUse : WeaponBehavior
	{
		public override void PerformAction()
		{
			// Do nothing, may use for animation
		}
		
		public override void OnTriggerPressed()
		{
			Enabled = false;
			Weapon.SetChargePercent(0);
		}
		
		public override void OnTriggerRelease()
		{
		}
		
		public override void OnTriggerHeld()
		{
			if(Enabled)
			{
				Weapon.SetChargePercent(-1);
				Use();
			}
			else
			{
				Weapon.SetChargePercent(Weapon.GetChargePercent() + Time.deltaTime);
				Enabled = (Weapon.GetChargePercent() >= Weapon.Weapon.ChargeTime.ModifiedValue);
			}
		}
	}
}
