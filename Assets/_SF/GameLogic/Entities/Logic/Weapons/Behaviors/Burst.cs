using UnityEngine;
using System.Collections;
using SF.GameLogic.Entities.Logic.Weapons.Internal;
using SF.GameLogic.Data.Enums;
using SF.Utilities.Managers;

namespace SF.GameLogic.Entities.Logic.Weapons.Behaviors
{
	public class Burst : WeaponBehavior
	{
		public override void PerformAction()
		{
			// Do nothing, may use for animation
		}
		
		public override void OnTriggerPressed()
		{
			Enabled = true;
			BurstUse();
		}
		
		public override void OnTriggerRelease()
		{
			if(_usageType == WeaponBehaviorUsageType.Actor)
			{
				BurstUse();
			}
		}
		
		public override void OnTriggerHeld()
		{
			if(_usageType == WeaponBehaviorUsageType.Actor)
			{
				BurstUse();
			}
		}
		
		private void BurstUse()
		{
			if(Weapon.CanUse())
			{
				GameManager.Instance.StartCoroutine(BurstRoutine());
			}
		}

		private IEnumerator BurstRoutine()
		{
			float burstTime = Weapon.Weapon.BurstTime.ModifiedValue;
			int burstCount = (int)Weapon.Weapon.BurstCount.ModifiedValue;
			float waitTime = burstTime / burstCount;
			
			Use();
			for(int i = 1; i < burstCount; i++)
			{
				yield return new WaitForSeconds(waitTime);
				Weapon.ResetNextTimeToUse();
				Use();
			}
			Enabled = false;
		}
	}
}
