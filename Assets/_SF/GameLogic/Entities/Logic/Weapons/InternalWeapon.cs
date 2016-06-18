namespace Weapons.Internal
{
	public interface InternalWeapon
	{
		Weapon Weapon { get; }
		void Use();
		void SetChargePercent(float chargeAmount);
		float GetChargePercent();
		void ResetNextTimeToUse();
		void Fire(float previousUseTime);
		bool CanUse();
	}
}
