using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SF.GameLogic.Entities.Logic.Weapons.Behaviors;
using SF.GameLogic.Entities.Logic.Weapons.States;
using SF.EventSystem;
using SF.GameLogic.Entities.Logic.Weapons.TriggerAdapters;
using SF.GameLogic.Data.Enums;
using SF.GameLogic.Data.Profiles;
using SF.GameLogic.Entities.Logic;
using SF.GameLogic.Projectiles;
using SF.GameLogic.EventSystem.EventData;
using SF.GameLogic.EventSystem.EventRegistrars;
using SF.Utilities.ModifiableAttributes;
using SF.Utilities;
using SF.CustomInspector.Attributes;

namespace SF.GameLogic.Entities.Logic.Weapons
{
	public class Weapon : Entity
	{
		[InspectorValue] public string Name { get; private set; }
		[InspectorValue] public AmmoType AmmoType { get; private set; }
		[InspectorObject] public ModifiableAttribute MaxAmmo { get; private set; }
		[InspectorObject] public ModifiableAttribute Accuracy { get; private set; }
		[InspectorObject] public ModifiableAttribute BaseDamage { get; private set; }
		[InspectorObject] public ModifiableAttribute RateOfFire { get; private set; }
		[InspectorObject] public ModifiableAttribute ReloadTime { get; private set; }
		[InspectorObject] public ModifiableAttribute AttackPower { get; private set; }
		[InspectorObject] public ModifiableAttribute ChargeTime { get; private set; }
		[InspectorObject] public ModifiableAttribute BurstTime { get; private set; }
		[InspectorObject] public ModifiableAttribute BurstCount { get; private set; }
		[InspectorObject] public ModifiableAttribute DeviationTime { get; private set; }
		[InspectorValue] public WeaponBehaviorType ActorBehaviorType { get; private set; }
		[InspectorValue] public WeaponBehaviorType TriggerBehaviorType { get; private set; }
		[InspectorValue] public Vector3 MinDeviation { get; private set; }
		[InspectorValue] public Vector3 MaxDeviation { get; private set; }
		[InspectorValue] public long PlayerEntityId { get; set; }

		[InspectorValue] public Transform FireTransform { get; private set; }
		public TriggerAdapter TriggerAdapter { get; private set; }

		private InternalWeapon _internalWeapon;
		private WeaponState _currentState;
		private bool _initialized = false;
		private float _chargePercent = -1;
		private WeaponBehavior _rangeAttackBehavior;
		private Vector3 _targetPosition;

	    public float Damage
	    {
	        get
	        {
	            return CalculateDamage();
	        }
		}

		private void Init()
		{
			if(!_initialized)
			{
				_internalWeapon = new InternalWeapon(this);
				_currentState = new ReadyWeaponState(_internalWeapon);
				_currentState.CurrentAmmo = (int)MaxAmmo.ModifiedValue;
				_rangeAttackBehavior = WeaponBehavior.CreateWeaponBehavior(_internalWeapon);
			}
		}

		private Weapon()
		{
		}
		
		public static Weapon CreateFromProfile(WeaponProfile profile, Transform fireTransform)
		{
			var weapon = new Weapon();
			weapon._eventRegistar = new WeaponEventRegistrar(weapon);
			weapon.Name = profile.Name;
			weapon.AmmoType = profile.AmmoType;
			weapon.TriggerBehaviorType = profile.TriggerBehaviorType;
			weapon.ActorBehaviorType = profile.FireBehaviorType;
			weapon.MaxAmmo = ModifiableAttribute.Create(profile.MaxAmmo);
			weapon.Accuracy = ModifiableAttribute.Create(profile.Accuracy);
			weapon.ReloadTime = ModifiableAttribute.Create(profile.ReloadTime);
			weapon.RateOfFire = ModifiableAttribute.Create(profile.RateOfFire); // 3500 is about the max ROF (rounds per minute) for 30fps
			weapon.BaseDamage = ModifiableAttribute.Create(profile.BaseDamage);
			weapon.AttackPower = ModifiableAttribute.Create(profile.AttackPower);
			weapon.ChargeTime = ModifiableAttribute.Create(profile.ChargeTime);
			weapon.BurstTime = ModifiableAttribute.Create(profile.BurstTime);
			weapon.BurstCount = ModifiableAttribute.Create(profile.BurstCount);
			weapon.DeviationTime = ModifiableAttribute.Create(profile.DeviationTime);
			weapon.MinDeviation = (profile.MinimumDeviation != null) ? new Vector3(profile.MinimumDeviation.X, profile.MinimumDeviation.Y, profile.MinimumDeviation.Z) : Vector3.zero;
			weapon.MaxDeviation = (profile.MaximumDeviation != null) ? new Vector3(profile.MaximumDeviation.X, profile.MaximumDeviation.Y, profile.MaximumDeviation.Z) : Vector3.zero;
			weapon.FireTransform = fireTransform;
			weapon.TriggerAdapter = TriggerAdapter.Create(weapon);
			weapon.Init();

			return weapon;
		}
	    
		public void TriggerPulled(WeaponTriggerEventData eventData)
		{
			SetWeaponForUse();
			_targetPosition = eventData.TargetPosition;
			_rangeAttackBehavior.OnTriggerPressed();
	    }

		public void TriggerHeld(WeaponTriggerEventData eventData)
		{
			SetWeaponForUse();
			_targetPosition = eventData.TargetPosition;
			_rangeAttackBehavior.OnTriggerHeld();
	    }

		public void TriggerReleased(WeaponTriggerEventData eventData)
		{
			SetWeaponForUse();
			_targetPosition = eventData.TargetPosition;
			_rangeAttackBehavior.OnTriggerRelease();
	    }

	    protected float CalculateDamage()
	    {
			float chargePercent = (_chargePercent >= 0) ? _chargePercent : 1;
			// (1 + (AttackPower * SQRT(Level)) / AttackPowerReference) * BaseDamage * Accuracy * SQRT(Level)
			// TODO: Add Level
			float damage = chargePercent * (1 + (AttackPower.ModifiedValue * Mathf.Sqrt (1/*level*/)) / SF.GameLogic.Data.Constants.WeaponConstants.ATTACK_POWER_REFERENCE) * BaseDamage.ModifiedValue * Accuracy.ModifiedValue * Mathf.Sqrt (1);
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
			SFEventManager.FireEvent(new SFEventData { OriginId = EntityId, EventType = SFEventType.WeaponReloaded });
	    }

	    public void Disable()
	    {
			_currentState = _currentState.SwitchToDisableState();
	        _currentState.Disable();
	    }

		private void Fire(float previousUseTime)
		{
			if((Time.time - previousUseTime) < DeviationTime.ModifiedValue)
			{
				var dev = MyVector3.RandomShellVector(MinDeviation, MaxDeviation);
				dev.z = -50;
				_targetPosition += dev;
			}

			var projectile = ProjectileFactory.CreateProjectileFromProfile(FireTransform, _targetPosition);
			AddDamageToProjectile(projectile);
			SFEventManager.FireEvent(new SFEventData { OriginId = EntityId, EventType = SFEventType.WeaponFired });
		}
		
		private void AddDamageToProjectile(Projectile projectile)
		{
			projectile.DamageData = new DamageData {
				AttackerId = PlayerEntityId,
				Damage = Damage,
				DamageType = DamageType.Fire
			};
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

			public void Fire(float previousUseTime)
			{
				_weapon.Fire(previousUseTime);
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
