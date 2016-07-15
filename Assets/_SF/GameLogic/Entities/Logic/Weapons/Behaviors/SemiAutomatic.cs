namespace SF.GameLogic.Entities.Logic.Weapons.Behaviors
{
	public class SemiAuto : WeaponBehavior
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
			Enabled = false;
		}
		
		public override void OnTriggerHeld()
		{
			// Do nothing, not automatic
		}
	}
}
