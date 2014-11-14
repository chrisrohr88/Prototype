using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;
using System.IO;

public static class ProfileManager
{
    private static string PARENT_PROFILE_PATH = Application.dataPath + "/GraveDefender/SerializedFiles/";

    private static Dictionary<string, WeaponProfile> _weaponProfiles = new Dictionary<string, WeaponProfile>();

	static ProfileManager()
	{
	}

	public static void LoadProfiles(System.Action callback)
	{
        LoadWeaponProfiles();
		callback.SafeInvoke();
	}

    private static void LoadWeaponProfiles()
    {
        var files = Directory.GetFiles(PARENT_PROFILE_PATH + "Weapons/", "*.json");
        LoadProfilesFromSourceToDestination<WeaponProfile>(files, _weaponProfiles);
    }

    private static void LoadProfilesFromSourceToDestination<T>(string[] files, Dictionary<string, T> destination) where T : BaseProfile
    {
        foreach(var file in files)
        {
            var profileString = File.ReadAllText(file);
            T profile = new JsonReader().Read<T>(profileString) as T;
            Debug.Log("Profile: " + new JsonWriter().Write(profile));
            destination.Add(profile.Name, profile);
        }
    }

    public static WeaponProfile GetWeaponProfileByName(string name)
    {
        return _weaponProfiles[name];
    }
}
