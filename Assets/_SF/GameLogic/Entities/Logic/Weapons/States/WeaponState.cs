namespace SF.GameLogic.Entities.Logic.Weapons.States
{
	public interface WeaponState
	{
	    int CurrentAmmo { get; set; }

	    void Use();
	    void Reload();
	    void Disable();
		void Ready();
		void ResetNextTimeToUse();
		bool CanUse();
	    WeaponState SwitchToReadyState();
	    WeaponState SwitchToReloadState();
	    WeaponState SwitchToDisableState();
	}
}
