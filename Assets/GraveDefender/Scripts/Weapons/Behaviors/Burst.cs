using UnityEngine;
using System.Collections;

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
	}
	
	private void BurstUse()
	{
		if(Weapon.CanUse())
		{
			GameManager.Instance.StartCoroutine(BurstRoutine());
		}
		Enabled = false;
	}

	private IEnumerator BurstRoutine()
	{
		float burstTime = Weapon.GetWeapon().BurstTime.ModifiedValue;
		int burstCount = (int)Weapon.GetWeapon().BurstCount.ModifiedValue;
		float waitTime = burstTime / burstCount;
		
		Use();
		for(int i = 1; i < burstCount; i++)
		{
			yield return new WaitForSeconds(waitTime);
			Weapon.ResetNextTimeToUse();
			Use();
		}
	}
}
