using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.Behaviors;
using Weapons.Enums;
using Weapons.States;

namespace Weapons
{
	public class Weapon
	{
		public string Name { get; private set; }
		public AmmoType AmmoType { get; private set; }
		public ModifiableAttribute MaxAmmo { get; private set; }
		public ModifiableAttribute Accuracy { get; private set; }
		public ModifiableAttribute BaseDamage { get; private set; }
		public ModifiableAttribute RateOfFire { get; private set; }
		public ModifiableAttribute ReloadTime { get; private set; }
		public ModifiableAttribute AttackPower { get; private set; }
		public ModifiableAttribute ChargeTime { get; private set; }
		public ModifiableAttribute BurstTime { get; private set; }
		public ModifiableAttribute BurstCount { get; private set; }
		public ModifiableAttribute DeviationTime { get; private set; }
		public WeaponBehaviorType ActorBehaviorType { get; private set; }
		public WeaponBehaviorType TriggerBehaviorType { get; private set; }
		public Vector3 MinDeviation { get; private set; }
		public Vector3 MaxDeviation { get; private set; }

		public Transform FireTransform { get; private set; }

		private InternalWeapon _internalWeapon;
		private WeaponState _currentState;
		private bool _initialized = false;
		private float _chargePercent = -1;
		private WeaponBehavior _rangeAttackBehavior;

	    public float Damage
	    {
	        get
	        {
	            return CalculateDamage();
	        }
		}

		public void Init()
		{
			if(!_initialized)
			{
				_internalWeapon = new InternalWeapon(this);
				_currentState = new ReadyWeaponState(_internalWeapon);
				_currentState.CurrentAmmo = (int)MaxAmmo.ModifiedValue;
				_rangeAttackBehavior = WeaponBehavior.CreateWeaponBehavior(_internalWeapon);

				SetupTriggers();
			}
		}

		private Weapon()
		{
		}
		
		public static Weapon CreateFromProfile(WeaponProfile profile, Transform fireTransform)
		{
			var newWeapon = new Weapon();		
			newWeapon.Name = profile.Name;
			newWeapon.AmmoType = profile.AmmoType;
			newWeapon.TriggerBehaviorType = profile.TriggerBehaviorType;
			newWeapon.ActorBehaviorType = profile.FireBehaviorType;
			newWeapon.MaxAmmo = ModifiableAttribute.Create(profile.MaxAmmo);
			newWeapon.Accuracy = ModifiableAttribute.Create(profile.Accuracy);
			newWeapon.ReloadTime = ModifiableAttribute.Create(profile.ReloadTime);
			newWeapon.RateOfFire = ModifiableAttribute.Create(profile.RateOfFire); // 3500 is about the max ROF (rounds per minute) for 30fps
			newWeapon.BaseDamage = ModifiableAttribute.Create(profile.BaseDamage);
			newWeapon.AttackPower = ModifiableAttribute.Create(profile.AttackPower);
			newWeapon.ChargeTime = ModifiableAttribute.Create(profile.ChargeTime);
			newWeapon.BurstTime = ModifiableAttribute.Create(profile.BurstTime);
			newWeapon.BurstCount = ModifiableAttribute.Create(profile.BurstCount);
			newWeapon.DeviationTime = ModifiableAttribute.Create(profile.DeviationTime);
			newWeapon.MinDeviation = (profile.MinimumDeviation != null) ? new Vector3(profile.MinimumDeviation.X, profile.MinimumDeviation.Y, profile.MinimumDeviation.Z) : Vector3.zero;
			newWeapon.MaxDeviation = (profile.MaximumDeviation != null) ? new Vector3(profile.MaximumDeviation.X, profile.MaximumDeviation.Y, profile.MaximumDeviation.Z) : Vector3.zero;
			newWeapon.FireTransform = fireTransform;
			newWeapon.Init ();

			return newWeapon;
		}

		//TODO: Redo Input solution
	    private void SetupTriggers()
		{
			FieldInteractable.OnHeld += TriggerHeld;
	        FieldInteractable.OnMoved += TriggerHeld;
			FieldInteractable.OnPressed += TriggerPulled;
			FieldInteractable.OnReleased += TriggerReleased;
	    }

		public void UnsubscribeEvents()
		{
			FieldInteractable.OnHeld -= TriggerHeld;
			FieldInteractable.OnMoved -= TriggerHeld;
			FieldInteractable.OnPressed -= TriggerPulled;
			FieldInteractable.OnReleased -= TriggerReleased;
		}
	    
	    public void TriggerPulled()
		{
			SetWeaponForUse();
			_rangeAttackBehavior.OnTriggerPressed();
	    }

	    public void TriggerHeld()
		{
			SetWeaponForUse();
			_rangeAttackBehavior.OnTriggerHeld();
	    }

	    public void TriggerReleased()
		{
			SetWeaponForUse();
			_rangeAttackBehavior.OnTriggerRelease();
	    }

	    protected float CalculateDamage()
	    {
			float chargePercent = (_chargePercent >= 0) ? _chargePercent : 1;
			// (1 + (AttackPower * SQRT(Level)) / AttackPowerReference) * BaseDamage * Accuracy * SQRT(Level)
			// TODO: Add Level
			float damage = chargePercent * (1 + (AttackPower.ModifiedValue * Mathf.Sqrt (1/*level*/)) / Constants.Weapon.ATTACK_POWER_REFERENCE) * BaseDamage.ModifiedValue * Accuracy.ModifiedValue * Mathf.Sqrt (1);
	//		Debug.LogError("Damage: " + damage + " ChargedPercent: " + chargePercent);
			return damage;
	    }

		private void Use()
		{
			_currentState.Use();
		}
	    
	    private void SetWeaponForUse()
	    {
			_currentState = _currentState.SwitchToReadyState();
		}

		public void Ready()
		{
			_currentState = _currentState.SwitchToReadyState();
			_currentState.Ready();
		}
		
		public void Reload()
	    {
			_currentState = _currentState.SwitchToReloadState();
	        _currentState.Reload();
	    }

	    public void Disable()
	    {
			_currentState = _currentState.SwitchToDisableState();
	        _currentState.Disable();
	    }
		
		private class InternalWeapon : Internal.InternalWeapon
		{
			public Weapon _weapon;

			public Weapon Weapon
			{
				get
				{
					return _weapon;
				}
			}

			public InternalWeapon(Weapon weapon)
			{
				_weapon = weapon;
			}
			
			public void ResetNextTimeToUse()
			{
				_weapon._currentState.ResetNextTimeToUse();
			}
			
			public void Use()
			{
				_weapon.Use();
			}
			
			public void SetChargePercent(float chargeAmount)
			{
				_weapon._chargePercent = chargeAmount;
			}
			
			public float GetChargePercent()
			{
				return _weapon._chargePercent;
			}
			
			public bool CanUse()
			{
				return _weapon._currentState.CanUse();
			}
		}
	}
}
