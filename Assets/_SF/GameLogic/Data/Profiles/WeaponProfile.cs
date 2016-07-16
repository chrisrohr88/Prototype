using SF.GameLogic.Data.Enums;
using SF.Utilities;

namespace SF.GameLogic.Data.Profiles
{
	public class WeaponProfile : BaseProfile
	{
		public int BaseDamage;
		public int RateOfFire;
		public int AttackPower;
		public float Accuracy;
		public int BurstCount;
		public float BurstTime;
		public float ChargeTime;
		public float ReloadTime;
		public int MaxAmmo;
		public AmmoType AmmoType;
		public WeaponBehaviorType FireBehaviorType;
		public WeaponBehaviorType TriggerBehaviorType;
		public float DeviationTime;
		public MyVector3 MinimumDeviation;
		public MyVector3 MaximumDeviation;
	}
}
