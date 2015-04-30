using Weapons;
using Weapons.Behaviors;
using Weapons.Enums;

public class EnemyProfile : BaseProfile
{
	public int BaseDamage { get; set; }
	public int RateOfFire { get; set; }
	public int AttackPower { get; set; }
	public float Accuracy { get; set; }
	public int BurstCount { get; set; }
	public float BurstTime { get; set; }
	public float ChargeTime { get; set; }
	public float ReloadTime { get; set; }
	public int MaxAmmo { get; set; }
	public AmmoType AmmoType { get; set; }
	public WeaponBehaviorType FireBehaviorType { get; set; }
	public WeaponBehaviorType TriggerBehaviorType { get; set; }
	public float DeviationTime { get; set; }
	public MyVector3 MinimumDeviation { get; set; }
	public MyVector3 MaximumDeviation { get; set; }
}
