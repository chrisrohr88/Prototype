using UnityEngine;

public enum AmmoType
{
    Fire
}

public enum WeaponType
{
    Pistol
}

public class Weapon
{
    private const float MAX_DAMAGE = 50000;
	public string Name { get; protected set; }
    public ModifiableAttribute BaseDamage { get; protected set; }
	public ModifiableAttribute RateOfFire { get; protected set; }
	public ModifiableAttribute AttackPower { get; protected set; }
	public ModifiableAttribute Accuracy { get; protected set; }
    public ModifiableAttribute ReloadTime { get; protected set; }
    public ModifiableAttribute MaxAmmo { get; protected set; }
    public AmmoType AmmoType { get; protected set; }

	private WeaponBehavior _fireBehavior;
    private WeaponState _currentState;

    public float Damage
    {
        get
        {
            return CalculateDamage();
        }
	}
	
	// TODO: Weapon factory
	public static Weapon CreateDeault()
	{
		var newWeapon = new Weapon();
		newWeapon._currentState = new ReadyWeaponState(newWeapon);
		newWeapon.Name = "Default";
		newWeapon.BaseDamage = ModifiableAttribute.Create(45);
		newWeapon.RateOfFire = ModifiableAttribute.Create(250); // 3500 is about the max ROF (rounds per minute) for 30fps
		newWeapon.AttackPower = ModifiableAttribute.Create(100);
		newWeapon.Accuracy = ModifiableAttribute.Create(.90f);
		newWeapon.ReloadTime = ModifiableAttribute.Create(2);
		newWeapon.MaxAmmo = ModifiableAttribute.Create(10);
		newWeapon.AmmoType = AmmoType.Fire;
		newWeapon._currentState.CurrentAmmo = (int)newWeapon.MaxAmmo.ModifiedValue;
		newWeapon.SetupTriggers();
		return newWeapon;
	}
	public static Weapon CreateFromProfile(WeaponProfile profile)
	{
		var newWeapon = new Weapon();
		newWeapon.Name = profile.Name;
		newWeapon._currentState = new ReadyWeaponState(newWeapon);
		newWeapon.BaseDamage = ModifiableAttribute.Create(profile.BaseDamage);
		newWeapon.RateOfFire = ModifiableAttribute.Create(profile.RateOfFire); // 3500 is about the max ROF (rounds per minute) for 30fps
		newWeapon.AttackPower = ModifiableAttribute.Create(profile.AttackPower);
		newWeapon.Accuracy = ModifiableAttribute.Create(profile.Accuracy);
		newWeapon.ReloadTime = ModifiableAttribute.Create(profile.ReloadTime);
		newWeapon.MaxAmmo = ModifiableAttribute.Create(profile.MaxAmmo);
		newWeapon.AmmoType = profile.AmmoType;
		newWeapon._currentState.CurrentAmmo = (int)newWeapon.MaxAmmo.ModifiedValue;
		newWeapon.SetupTriggers();
		return newWeapon;
	}
	
	public static Weapon CreateFromWeapon(Weapon weapon)
	{
		var newWeapon = new Weapon();
		newWeapon.Name = weapon.Name;
		newWeapon.BaseDamage = weapon.BaseDamage;
		newWeapon.RateOfFire = weapon.RateOfFire;
		newWeapon.AttackPower = weapon.AttackPower;
		newWeapon.Accuracy = weapon.Accuracy;
		newWeapon.ReloadTime = weapon.ReloadTime;
		newWeapon.MaxAmmo = weapon.MaxAmmo;
		//        newWeapon._projectilePrefab = weapon._projectilePrefab;
		newWeapon.AmmoType = weapon.AmmoType;
		//        newWeapon._projectileSpawnPoint = weapon._projectileSpawnPoint;
		newWeapon.SetupTriggers();
		return newWeapon;
	}

	//TODO: Redo Input solution
    private void SetupTriggers()
    {
        FieldInteractable.OnPressed += TriggerPulled;
        FieldInteractable.OnReleaseed += TriggerReleased;
        FieldInteractable.OnMoved += TriggerHeld;
        FieldInteractable.OnHeld += TriggerHeld;
    }

	public void UnsubscribeEvents()
	{
		FieldInteractable.OnPressed -= TriggerPulled;
		FieldInteractable.OnReleaseed -= TriggerReleased;
		FieldInteractable.OnMoved -= TriggerHeld;
		FieldInteractable.OnHeld -= TriggerHeld;
	}
    
    public void TriggerPulled()
	{
		Use();
    }

    public void TriggerHeld()
	{
		Use();
    }

    public void TriggerReleased()
	{
		Use();
    }

    protected float CalculateDamage()
    {
		// (1 + (AttackPower * SQRT(Level)) / AttackPowerReference) * BaseDamage * Accuracy * SQRT(Level)
		// TODO: Add Level & AttackPowerReference
		return (1 + (AttackPower.ModifiedValue * Mathf.Sqrt (1)) / Constants.Weapon.ATTACK_POWER_REFERENCE) * BaseDamage.ModifiedValue * Accuracy.ModifiedValue * Mathf.Sqrt (1);
    }
    
    public void Ready()
    {
        _currentState = _currentState.SwitchToReadyState();
        _currentState.Ready();
    }
    
    public void Use()
    {
        _currentState = _currentState.SwitchToReadyState();
        _currentState.Use();
    }

    public void Reload()
    {
        _currentState = _currentState.SwitchToReloadState();
        _currentState.Reload();
    }

    public void Disable()
    {
        _currentState = _currentState.SwitchToDisableState();
        _currentState.Disable();
    }
}
