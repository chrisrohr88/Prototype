public class RapidFire : WeaponBehavior
{
	public override void PerformAction()
	{
		// Do nothing, may use for animation
	}
	
	public override void OnTriggerPressed()
	{
		Enabled = true;
		Use();
	}
	
	public override void OnTriggerRelease()
	{
		Weapon.ResetNextTimeToUse();
		Enabled = false;
	}
	
	public override void OnTriggerHeld()
	{
		// Do nothing, not automatic
	}
}
