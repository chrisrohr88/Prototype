using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons.Behaviors;
using Weapons.Enums;
using Weapons.States;
using SF.EventSystem;
using Weapons.TriggerAdapters;

namespace Weapons
{
	public class Weapon : Entity
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
		public long PlayerEntityId { get; set; }

		public Transform FireTransform { get; private set; }
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
			var newWeapon = new Weapon();
			newWeapon.SetupEventRegistar();	
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
			newWeapon.TriggerAdapter = TriggerAdapter.Create(newWeapon);
			newWeapon.Init();

			return newWeapon;
		}

		// TODO : want to make this datadriven & move to Entity
		private	List<SFEvent> CreateEventsToRegister()
		{
			return new List<SFEvent>
			{
				new SFEvent { OriginId = EntityId, EventType = SFEventType.WeaponFired },
				new SFEvent { OriginId = EntityId, EventType = SFEventType.WeaponReloaded },
				new SFEvent { OriginId = EntityId, EventType = SFEventType.WeaponTriggerHold },
				new SFEvent { OriginId = EntityId, EventType = SFEventType.WeaponTriggerPull },
				new SFEvent { OriginId = EntityId, EventType = SFEventType.WeaponTriggerRelease }
			};
		}

		private void SetupEventRegistar()
		{
			_eventRegistar = new EventRegistar(CreateEventsToRegister());
			_eventRegistar.RegisterEvents();
			SFEventManager.RegisterEventListner(SFEventType.WeaponTriggerHold, new ConcreteSFEventListner<WeaponTriggerEventData> { TargetId = EntityId, MethodToExecute = TriggerHeld });
			SFEventManager.RegisterEventListner(SFEventType.WeaponTriggerPull, new ConcreteSFEventListner<WeaponTriggerEventData> { TargetId = EntityId, MethodToExecute = TriggerPulled });
			SFEventManager.RegisterEventListner(SFEventType.WeaponTriggerRelease, new ConcreteSFEventListner<WeaponTriggerEventData> { TargetId = EntityId, MethodToExecute = TriggerReleased });
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
