namespace SF.GameLogic.Entities.Logic.Weapons.Behaviors
{
	public class NullBehavior :  WeaponBehavior
	{
		public override void PerformAction()
		{
			// Do nothing, may use for animation
		}
		
		public override void OnTriggerPressed()
		{
			// Do nothing, NULL
		}
		
		public override void OnTriggerRelease()
		{
			// Do nothing, NULL
		}
		
		public override void OnTriggerHeld()
		{
			// Do nothing, NULL
		}
	}
}
