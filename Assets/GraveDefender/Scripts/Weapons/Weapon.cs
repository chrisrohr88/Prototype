public enum AmmoType
{
    None
}

public enum WeaponType
{
    Pistol
}

public abstract class Weapon
{
    private const float MAX_DAMAGE = 50000;
    public ModifiableAttribute BaseDamage { get; protected set; }
    public ModifiableAttribute RateOfFire { get; protected set; }
    public ModifiableAttribute AttackPower { get; protected set; }
    public ModifiableAttribute ReloadTime { get; protected set; }
    public ModifiableAttribute MaxAmmo { get; protected set; }
    public ModifiableAttribute ProjectileSpeed { get; protected set; }
    public AmmoType AmmoType { get; protected set; }
    private WeaponState _currentState;

    public float Damage
    {
        get
        {
            return CalculateDamage();
        }
    }

    // TODO: Weapon factory
    public static Weapon CreateDeault<T>() where T : Weapon, new()
    {
        var newWeapon = new T();
        newWeapon._currentState = new ReadyWeaponState(newWeapon);
        newWeapon.BaseDamage = ModifiableAttribute.Create(45);
        newWeapon.RateOfFire = ModifiableAttribute.Create(250); // 3500 is about the max ROF (rounds per minute) for 30fps
        newWeapon.AttackPower = ModifiableAttribute.Create(100);
        newWeapon.ReloadTime = ModifiableAttribute.Create(2);
        newWeapon.MaxAmmo = ModifiableAttribute.Create(10);
        newWeapon.AmmoType = AmmoType.None;
        newWeapon._currentState.CurrentAmmo = (int)newWeapon.MaxAmmo.ModifiedValue;
        newWeapon.SetupTriggers();
        return newWeapon;
    }

    public static Weapon CreateFromWeapon<T>(Weapon weapon) where T : Weapon, new()
    {
        var newWeapon = new T();
        newWeapon.BaseDamage = weapon.BaseDamage;
        newWeapon.RateOfFire = weapon.RateOfFire;
        newWeapon.AttackPower = weapon.AttackPower;
        newWeapon.ReloadTime = weapon.ReloadTime;
        newWeapon.MaxAmmo = weapon.MaxAmmo;
        newWeapon.ProjectileSpeed = weapon.ProjectileSpeed;
//        newWeapon._projectilePrefab = weapon._projectilePrefab;
        newWeapon.AmmoType = weapon.AmmoType;
//        newWeapon._projectileSpawnPoint = weapon._projectileSpawnPoint;
        newWeapon.SetupTriggers();
        return newWeapon;
    }

    private void SetupTriggers()
    {
        FieldInteractable.OnPressed += TriggerPulled;
        FieldInteractable.OnReleaseed += TriggerReleased;
        FieldInteractable.OnMoved += TriggerHeld;
        FieldInteractable.OnHeld += TriggerHeld;
    }
    
    public virtual void TriggerPulled()
    {
        // Don't do anything
    }

    public virtual void TriggerHeld()
    {
        // Don't do anything
    }

    public virtual void TriggerReleased()
    {
        // Don't do anything
    }

    protected virtual float CalculateDamage()
    {
        return BaseDamage.ModifiedValue;
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
