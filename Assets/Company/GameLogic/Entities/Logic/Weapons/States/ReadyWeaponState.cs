using UnityEngine;
using System.Collections;
using Weapons.Internal;

namespace Weapons.States
{
	public class ReadyWeaponState : WeaponState
	{
		private InternalWeapon _weapon;
		private float _timeToUseNext;
		private float _previousUseTime = int.MinValue;

	    
	    public int CurrentAmmo { get; set; }
	    
	    public ReadyWeaponState(InternalWeapon weapon)
	    {
	        _weapon = weapon;
	    }
	    
	    public void Ready()
	    {
	        CurrentAmmo = (int)_weapon.Weapon.MaxAmmo.ModifiedValue;
	    }
	    
	    public void Use()
	    {
	        if(CanUse())
			{
	            UpdateUseTimes();
	            CreateProjectile();
	            UpdateAmmo();
	        }
	    }
	    
	    public bool CanUse()
	    {
	        return ((_timeToUseNext <= Time.time) && (CurrentAmmo > 0));
	    }
	    
	    private void CreateProjectile()
	    {
			var spawnPosition = _weapon.Weapon.FireTransform.position;

			if((Time.time - _previousUseTime) < _weapon.Weapon.DeviationTime.ModifiedValue)
			{
				var dev = MyVector3.RandomShellVector(_weapon.Weapon.MinDeviation, _weapon.Weapon.MaxDeviation);
				dev.z = -50;
				spawnPosition += dev;
			}
			var projectile = Projectile.Create(_weapon.Weapon.FireTransform);
	        AddDamageToProjectile(projectile);
	    }
	    
	    private void AddDamageToProjectile(Projectile projectile)
	    {
	        var damageData = projectile.gameObject.AddComponent<DamageData>();
	        damageData.AttackerId = 1; // TODO get real id
	        damageData.Damage = _weapon.Weapon.Damage;
	        damageData.DamageType = DamageType.Fire;
	    }
	    
	    private void UpdateUseTimes()
	    {
			_previousUseTime = Time.time;
	        _timeToUseNext = Time.time + (60f / _weapon.Weapon.RateOfFire.ModifiedValue);
		}
		
		public void ResetNextTimeToUse()
		{
			_timeToUseNext = Time.time;
		}
		
		public bool WaitForUse()
		{
			return Time.time < _timeToUseNext;
		}

	    private void UpdateAmmo()
	    {
	        CurrentAmmo--;
	        CheckToReload();
	    }
	    
	    private void CheckToReload()
	    {
	        if (CurrentAmmo <= 0)
	        {
	            _weapon.Weapon.Reload();
	        }
	    }
	    
	    public void Reload()
	    {
	    }
	    
	    public void Disable()
	    {
	    }
	    
	    public WeaponState SwitchToReadyState()
	    {
	        return this;
	    }
	    
	    public WeaponState SwitchToReloadState()
	    {
	        return new ReloadWeaponState(_weapon);
	    }
	    
	    public WeaponState SwitchToDisableState()
	    {
	        return new DisableWeaponState(_weapon);
	    }
	}
}
