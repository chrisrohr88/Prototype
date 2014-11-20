using UnityEngine;
using System.Collections;

public class ReadyWeaponState : WeaponState
{
    private Weapon _weapon;
    private float _timeToFireNext;
    
    public int CurrentAmmo { get; set; }
    
    public ReadyWeaponState(Weapon weapon)
    {
        _weapon = weapon;
    }
    
    public void Ready()
    {
        CurrentAmmo = (int)_weapon.MaxAmmo.ModifiedValue;
    }
    
    public void Use()
    {
        if(CanUse())
        {
            UpdateNextTimeToUse();
            CreateProjectile();
            UpdateAmmo();
        }
    }
    
    private bool CanUse()
    {
        return ((_timeToFireNext < Time.time) && (CurrentAmmo > 0));
    }
    
    private void CreateProjectile()
    {
        var projectile = Projectile.Create(GameMode.FireTransform);
        AddDamageToProjectile(projectile);
    }
    
    private void AddDamageToProjectile(Projectile projectile)
    {
        var damageData = projectile.gameObject.AddComponent<DamageData>();
        damageData.AttackerId = 1;
        damageData.Damage = _weapon.Damage;
        damageData.DamageType = DamageType.Fire;
    }
    
    private void UpdateNextTimeToUse()
    {
        _timeToFireNext = Time.time + (60f / _weapon.RateOfFire.ModifiedValue);
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
            _weapon.Reload();
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
