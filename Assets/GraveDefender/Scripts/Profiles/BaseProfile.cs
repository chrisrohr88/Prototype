using UnityEngine;
using System.Collections;
using JsonFx.Json;

public class WeaponProfile : BaseProfile
{
	public int BaseDamage { get; set; }
	public int RateOfFire { get; set; }
	public int AttackPower { get; set; }
	public float ReloadTime { get; set; }
	public AmmoType AmmoType { get; set; }
	public int MaxAmmo { get; set; }
	public int Accuracy { get; set; }
}

public abstract class BaseProfile
{
	public string Name { get; set; }
}
