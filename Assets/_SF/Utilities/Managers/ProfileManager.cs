using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;
using System.IO;
using SF.GameLogic.Data.Profiles;
using SF.Utilities.Extensions;

namespace SF.Utilities.Managers
{
	public static class ProfileManager
	{
		private static string PARENT_PROFILE_PATH = "SerializedFiles/";
		
		private static Dictionary<string, WeaponProfile> _weaponProfiles = new Dictionary<string, WeaponProfile>();
		private static Dictionary<string, EnemyProfile> _enemyProfiles = new Dictionary<string, EnemyProfile>();

		private static bool _profilesLoaded = false;

		static ProfileManager()
		{
		}

		public static void LoadProfiles(System.Action callback)
		{
			if(!_profilesLoaded)
			{
				LoadWeaponProfiles();
				LoadEnemyProfiles();
				_profilesLoaded = true;
			}
			callback.SafeInvoke();
		}

	    private static void LoadWeaponProfiles()
		{
			var files = Resources.LoadAll<TextAsset>(PARENT_PROFILE_PATH + "Weapons/");
	        LoadProfilesFromSourceToDestination<WeaponProfile>(files, _weaponProfiles);
		}
		
		private static void LoadEnemyProfiles()
		{
			var files = Resources.LoadAll<TextAsset>(PARENT_PROFILE_PATH + "Enemies/");
			LoadProfilesFromSourceToDestination<EnemyProfile>(files, _enemyProfiles);
		}

		private static void LoadProfilesFromSourceToDestination<T>(TextAsset[] files, Dictionary<string, T> destination) where T : BaseProfile
	    {
	        foreach(var file in files)
	        {
	            var profileString = file.text;
	            T profile = new JsonReader().Read<T>(profileString) as T;
	            destination.Add(profile.Name, profile);
	        }
	    }

		public static EnemyProfile GetEnemyProfile(string name)
		{
			return _enemyProfiles[name];
		}

		public static WeaponProfile GetRandomWeapon()
		{
			var rand = Random.Range (0, _weaponProfiles.Count);
			WeaponProfile profile = _weaponProfiles ["Pistol"];
			int i = 0;
			foreach(var weaponProfile in _weaponProfiles.Values)
			{
				if(rand == i++)
				{
					profile = weaponProfile;
				}
			}

			return profile;
		}

	    public static WeaponProfile GetWeaponProfileByName(string name)
	    {
	        return _weaponProfiles[name];
	    }
	}
}
