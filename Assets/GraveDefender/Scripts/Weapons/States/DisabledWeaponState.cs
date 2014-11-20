

public class DisableWeaponState : WeaponState
{
    private Weapon _weapon;
    private float _timeToFireNext;
    
    public int CurrentAmmo { get; set; }
    
    public DisableWeaponState(Weapon weapon)
    {
        _weapon = weapon;
    }
    
    public void Ready()
    {
    }
    
    public void Use()
    {
    }
    
    public void Reload()
    {
    }
    
    public void Disable()
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