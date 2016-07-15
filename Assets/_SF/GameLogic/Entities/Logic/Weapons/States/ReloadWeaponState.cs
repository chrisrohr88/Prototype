using UnityEngine;
using System.Collections;
using SF.GameLogic.Entities.Logic.Weapons.Internal;
using SF.Utilities.Managers;

namespace SF.GameLogic.Entities.Logic.Weapons.States
{
	public class ReloadWeaponState : WeaponState
	{
	    private InternalWeapon _weapon;
		private bool _reloading = false;
	    
	    public int CurrentAmmo { get; set; }
	    
	    public ReloadWeaponState(InternalWeapon weapon)
	    {
	        _weapon = weapon;
	    }
	    
	    public void Ready()
	    {
	    }
	    
	    public void Use()
	    {
		}
		
		public bool CanUse()
		{
			return false;
		}
	    
	    public void Reload()
	    {
			if(!_reloading)
			{
	        	GameManager.Instance.StartCoroutine(ReloadRoutine());
			}
	    }
	    
	    private IEnumerator ReloadRoutine()
	    {
			_reloading = true;
			yield return new WaitForSeconds (Mathf.Max (0f, _weapon.Weapon.ReloadTime.ModifiedValue));
			_reloading = false;
	        _weapon.Weapon.Ready();
	    }
	    
	    public void Disable()
	    {
		}
		
		public void ResetNextTimeToUse()
		{
		}
	    
	    public WeaponState SwitchToReadyState()
	    {
			if(!_reloading)
			{
	        	return new ReadyWeaponState(_weapon);
			}

			return this;
	    }
	    
	    public WeaponState SwitchToReloadState()
	    {
	        return this;
	    }
	    
	    public WeaponState SwitchToDisableState()
		{
			if(!_reloading)
			{
	        	return new DisableWeaponState(_weapon);
			}

			return this;
	    }
	}
}
