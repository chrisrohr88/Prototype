namespace Weapons.Behaviors
{
	public class AI : WeaponBehavior
	{
		public override void PerformAction()
		{
			// Do nothing, may use for animation
		}
		
		public override void OnTriggerPressed()
		{
			Enabled = true;
			Use();
			Enabled = false;
		}
		
		public override void OnTriggerRelease()
		{
		}
		
		public override void OnTriggerHeld()
		{
		}
	}
}
