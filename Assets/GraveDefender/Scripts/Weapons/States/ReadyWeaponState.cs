using UnityEngine;
using System.Collections;

public class ReadyWeaponState : WeaponState
{
	private IWeapon _weapon;
	private float _timeToUseNext;
	private float _previousUseTime = int.MinValue;

    
    public int CurrentAmmo { get; set; }
    
    public ReadyWeaponState(IWeapon weapon)
    {
        _weapon = weapon;
    }
    
    public void Ready()
    {
        CurrentAmmo = (int)_weapon.GetWeapon().MaxAmmo.ModifiedValue;
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

	private Vector3 RandomVector3()
	{
		return new Vector3(Random.value, Random.value, Random.value);
	}

	private Vector3 RandomVector3Range(Vector3 min, Vector3 max)
	{
		return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
	}
    
    private void CreateProjectile()
    {
		var spawnPosition = GameMode.FireTransform.position;

		// TODO make this better. Deviation range not positive range
		if((Time.time - _previousUseTime) < _weapon.GetWeapon().DeviationTime.ModifiedValue)
		{
			spawnPosition += RandomVector3Range(_weapon.GetWeapon().MinDeviation, _weapon.GetWeapon().MaxDeviation);
		}
		var projectile = Projectile.Create(spawnPosition);
        AddDamageToProjectile(projectile);
    }
    
    private void AddDamageToProjectile(Projectile projectile)
    {
        var damageData = projectile.gameObject.AddComponent<DamageData>();
        damageData.AttackerId = 1; // TODO get real id
        damageData.Damage = _weapon.GetWeapon().Damage;
        damageData.DamageType = DamageType.Fire;
    }
    
    private void UpdateUseTimes()
    {
		_previousUseTime = Time.time;
        _timeToUseNext = Time.time + (60f / _weapon.GetWeapon().RateOfFire.ModifiedValue);
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
            _weapon.GetWeapon().Reload();
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
