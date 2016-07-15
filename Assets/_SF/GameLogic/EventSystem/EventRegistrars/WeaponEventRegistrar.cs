using SF.GameLogic.Entities.Logic.Weapons;
using SF.GameLogic.EventSystem.EventData;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventRegistrars
{
	public class WeaponEventRegistrar : EventRegistrar
	{
		private Weapon _weapon;

		public WeaponEventRegistrar(Weapon weapon)
		{
			_weapon = weapon;
			Register();
		}

		protected override void RegisterEvents()
		{
			RegisterEvent(SFEventType.WeaponFired, _weapon.EntityId);
			RegisterEvent(SFEventType.WeaponReloaded, _weapon.EntityId);
			RegisterEvent(SFEventType.WeaponTriggerHold, _weapon.EntityId);
			RegisterEvent(SFEventType.WeaponTriggerPull, _weapon.EntityId);
			RegisterEvent(SFEventType.WeaponTriggerRelease, _weapon.EntityId);
		}

		protected override void RegisterEventListeners()
		{
			RegisterEventListener(SFEventType.WeaponTriggerHold, new ConcreteSFEventListener<WeaponTriggerEventData> { TargetId = _weapon.EntityId, MethodToExecute = _weapon.TriggerHeld });
			RegisterEventListener(SFEventType.WeaponTriggerPull, new ConcreteSFEventListener<WeaponTriggerEventData> { TargetId = _weapon.EntityId, MethodToExecute = _weapon.TriggerPulled });
			RegisterEventListener(SFEventType.WeaponTriggerRelease, new ConcreteSFEventListener<WeaponTriggerEventData> { TargetId = _weapon.EntityId, MethodToExecute = _weapon.TriggerReleased });
		}
	}
}
