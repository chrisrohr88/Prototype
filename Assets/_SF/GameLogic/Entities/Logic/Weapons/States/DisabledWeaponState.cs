using Weapons.Internal;

namespace Weapons.States
{
	public class DisableWeaponState : WeaponState
	{
	    private InternalWeapon _weapon;
	    
	    public int CurrentAmmo { get; set; }
	    
	    public DisableWeaponState(InternalWeapon weapon)
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
	    }
	    
	    public void Disable()
	    {
		}
		
		public void ResetNextTimeToUse()
		{
		}
	    
	    public WeaponState SwitchToReadyState()
	    {
	        return new ReadyWeaponState(_weapon);
	    }
	    
	    public WeaponState SwitchToReloadState()
	    {
	        return new ReloadWeaponState(_weapon);
	    }
	    
	    public WeaponState SwitchToDisableState()
	    {
	        return this;
	    }
	}
}