using UnityEngine;
using SF.GameLogic.Entities.Logic.Weapons.Behaviors;
using SF.GameLogic.Data.Profiles;
using SF.GameLogic.Data.Enums;

namespace SF.GameLogic.Entities.Logic.Weapons
{
	public static class WeaponFactory
	{
		public static Weapon CreateDeault(Transform fireTransform)
		{
			var profile = new WeaponProfile();

			profile.FireBehaviorType = WeaponBehaviorType.Automatic;
			profile.Name = "Default";
			profile.BaseDamage = 45;
			profile.RateOfFire = 250; // 3500 is about the max ROF (rounds per minute) for 30fps
			profile.AttackPower = 100;
			profile.Accuracy = .90f;
			profile.ReloadTime = 2;
			profile.MaxAmmo = 10;
			profile.AmmoType = AmmoType.Fire;

			return CreateFromProfile(profile, fireTransform);
		}
		
		public static Weapon CreateFromProfile(WeaponProfile profile, Transform fireTransform)
		{
			return Weapon.CreateFromProfile(profile, fireTransform);

		}
	}
}
