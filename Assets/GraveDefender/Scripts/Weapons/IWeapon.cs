public interface IWeapon
{
	void Use();
	Weapon GetWeapon();
	void SetChargePercent(float chargeAmount);
	float GetChargePercent();
	void ResetNextTimeToUse();
	bool CanUse();
}
