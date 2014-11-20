public interface WeaponState
{
    int CurrentAmmo { get; set; }

    void Use();
    void Reload();
    void Disable();
    void Ready();
    WeaponState SwitchToReadyState();
    WeaponState SwitchToReloadState();
    WeaponState SwitchToDisableState();
}
