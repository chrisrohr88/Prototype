using UnityEngine;
using System.Collections;
using Weapons.Internal;
using Weapons.Behaviors.Enums;

namespace Weapons.Behaviors
{
	public abstract class WeaponBehavior
	{
		private bool _enabled;
		private bool _enabledOverridden;
		protected WeaponBehaviorUsageType _usageType;
		protected InternalWeapon Weapon { get; private set; }

		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			protected set
			{
				if(!_enabledOverridden)
				{
					_enabled = value;
				}
			}
		}

		public static WeaponBehavior CreateWeaponBehavior(InternalWeapon weapon)
		{
			WeaponBehavior weaponBehavior;
			if(weapon.GetWeapon().TriggerBehaviorType == WeaponBehaviorType.None)
			{
				weaponBehavior = CreateBehavior(weapon, weapon.GetWeapon().ActorBehaviorType);
			}
			else
			{
				weaponBehavior = CreateCompundWEaponBehavior(weapon);
			}
			weaponBehavior.Weapon = weapon;

			return weaponBehavior;
		}

		static WeaponBehavior CreateCompundWEaponBehavior(InternalWeapon weapon)
		{
			WeaponBehavior trigger = CreateBehavior(weapon, weapon.GetWeapon().TriggerBehaviorType, WeaponBehaviorUsageType.Trigger);
			WeaponBehavior actor = CreateBehavior(weapon, weapon.GetWeapon().ActorBehaviorType, WeaponBehaviorUsageType.Actor);
			CompoundWeaponBehavior compoundWeaponBehavior = new CompoundWeaponBehavior(trigger, actor, actor.SetEnableOverride);
			return compoundWeaponBehavior;
		}

		static WeaponBehavior CreateBehavior(InternalWeapon weapon, WeaponBehaviorType type, WeaponBehaviorUsageType usageType = WeaponBehaviorUsageType.None)
		{
			WeaponBehavior weaponBehavior;
			switch(type)
			{
				case WeaponBehaviorType.Automatic:
					weaponBehavior = new Automatic();
					break;
				case WeaponBehaviorType.SemiAuto:
					weaponBehavior = new SemiAuto();
					break;
				case WeaponBehaviorType.SemiAutoUnCapped:
					weaponBehavior = new RapidFire();
					break;
				case WeaponBehaviorType.OnRelease:
					weaponBehavior = new UseOnRelease();
					break;
				case WeaponBehaviorType.Charged:
					weaponBehavior = new ChargedUse();
					break;
				case WeaponBehaviorType.WarmFirst:
					weaponBehavior = new WarmBeforeUse();
					break;
				case WeaponBehaviorType.Burst:
					weaponBehavior = new Burst();
					break;
				case WeaponBehaviorType.Spread:
					weaponBehavior = new Spread();
					break;
				default:
					Debug.LogError("WeaponBehaviorType " + type.ToString() + " not implemented");
					weaponBehavior = new NullBehavior();
					break;
			}
			weaponBehavior._usageType = usageType;
			weaponBehavior.Weapon = weapon;
			return weaponBehavior;
		}
		
		protected void Use()
		{
			if(_usageType != WeaponBehaviorUsageType.Trigger && Enabled)
			{
				Weapon.Use();
			}
		}

		private void SetEnableOverride(bool enabled)
		{
			_enabled = enabled;
			_enabledOverridden = true;
		}

		public abstract void OnTriggerPressed();
		public abstract void OnTriggerRelease();
		public abstract void OnTriggerHeld();
		public abstract void PerformAction();
	}
}
