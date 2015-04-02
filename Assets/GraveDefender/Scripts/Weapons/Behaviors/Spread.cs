public class Spread : WeaponBehavior
{
	public override void PerformAction()
	{
		// Do nothing, may use for animation
	}
	
	public override void OnTriggerPressed()
	{
		Enabled = true;
		DoSpread();
	}
	
	public override void OnTriggerRelease()
	{
		Enabled = false;
	}
	
	public override void OnTriggerHeld()
	{
	}

	private void DoSpread()
	{
	}
}
