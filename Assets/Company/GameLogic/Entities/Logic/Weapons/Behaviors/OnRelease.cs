namespace Weapons.Behaviors
{
	public class UseOnRelease : WeaponBehavior
	{
		public override void PerformAction()
		{
			// Do nothing, may use for animation
		}
		
		public override void OnTriggerPressed()
		{
			Enabled = false;
		}
		
		public override void OnTriggerRelease()
		{
			Enabled = true;
			Use();
		}
		
		public override void OnTriggerHeld()
		{
		}
	}
}
