

public class DisableWeaponState : WeaponState
{
    private IWeapon _weapon;
    
    public int CurrentAmmo { get; set; }
    
    public DisableWeaponState(IWeapon weapon)
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