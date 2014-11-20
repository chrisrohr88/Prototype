using UnityEngine;
using System.Collections;

public class ReloadWeaponState : WeaponState
{
    private Weapon _weapon;
    private float _timeToFireNext;
    
    public int CurrentAmmo { get; set; }
    
    public ReloadWeaponState(Weapon weapon)
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
        GameManager.Instance.StartCoroutine(ReloadRoutine());
    }
    
    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(Mathf.Max(0f, _weapon.ReloadTime.ModifiedValue));
        _weapon.Ready();
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
        return this;
    }
    
    public WeaponState SwitchToDisableState()
    {
        return new DisableWeaponState(_weapon);
    }
}