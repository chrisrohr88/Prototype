public class CompoundWeaponBehavior : WeaponBehavior
{
	WeaponBehavior _trigger;
	WeaponBehavior _actor;
	System.Action<bool> _actorEnabledOverride;
	
	public CompoundWeaponBehavior(WeaponBehavior trigger, WeaponBehavior actor, System.Action<bool> actorEnableOverride)
	{
		_trigger = trigger;
		_actor = actor;
		_actorEnabledOverride = actorEnableOverride;
		_actorEnabledOverride.SafeInvoke(false);
	}
	
	public override void PerformAction()
	{
		_trigger.PerformAction();
		_actorEnabledOverride.SafeInvoke(_trigger.Enabled);
		_actor.PerformAction();
	}
	
	public override void OnTriggerPressed()
	{
		_trigger.OnTriggerPressed();
		_actorEnabledOverride.SafeInvoke(_trigger.Enabled);
		_actor.OnTriggerPressed();
	}
	
	public override void OnTriggerRelease()
	{
		_trigger.OnTriggerRelease();
		_actorEnabledOverride.SafeInvoke(_trigger.Enabled);
		_actor.OnTriggerRelease();
	}
	
	public override void OnTriggerHeld()
	{
		_trigger.OnTriggerHeld();
		_actorEnabledOverride.SafeInvoke(_trigger.Enabled);
		_actor.OnTriggerHeld();
	}
}
