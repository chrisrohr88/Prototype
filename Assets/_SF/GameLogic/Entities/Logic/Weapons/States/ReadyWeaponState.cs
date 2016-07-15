using UnityEngine;
using System.Collections;
using SF.GameLogic.Entities.Logic.Weapons.Internal;

namespace SF.GameLogic.Entities.Logic.Weapons.States
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
	            _weapon.Fire(_previousUseTime);
	            UpdateAmmo();
	        }
	    }
	    
	    public bool CanUse()
	    {
	        return ((_timeToUseNext <= Time.time) && (CurrentAmmo > 0));
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
