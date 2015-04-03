using UnityEngine;

public class ChargedUse : WeaponBehavior
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
		Enabled = true;
		Use();
	}
	
	public override void OnTriggerHeld()
	{
		float chargeAmountThisFrame = Time.deltaTime * Weapon.GetWeapon().ChargeTime.ModifiedValue;
		float previousChargeAmount = Weapon.GetChargePercent();
		Weapon.SetChargePercent(Mathf.Clamp01(chargeAmountThisFrame + previousChargeAmount));
	}
}