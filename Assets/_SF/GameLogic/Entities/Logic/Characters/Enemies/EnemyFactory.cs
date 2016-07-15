using UnityEngine;
using SF.GameLogic.Data.Profiles;
using SF.GameLogic.Entities.Logic.Weapons;
using SF.Utilities.Managers;

namespace SF.GameLogic.Entities.Logic.Charaters.Enemies
{
	public static class CharacterFactory
	{
		public static Enemy CreateEnemyFromProfile(EnemyProfile profile)
		{
			BaseEnemy baseEnemy = (GameObject.Instantiate(Resources.Load(profile.EnemyPrefabPath)) as GameObject).GetComponent<BaseEnemy>();
			Weapon weapon = WeaponFactory.CreateFromProfile(ProfileManager.GetWeaponProfileByName(profile.WeaponProfileName), baseEnemy.SpawnTransform);
			return Enemy.Create(profile, weapon, baseEnemy);
		}
	}
}
