using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;
using System.IO;

public static class ProfileManager
{
	private static string PARENT_PROFILE_PATH = "SerializedFiles/";

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
		var files = Resources.LoadAll<TextAsset>(PARENT_PROFILE_PATH + "Weapons/");
        LoadProfilesFromSourceToDestination<WeaponProfile>(files, _weaponProfiles);
    }

	private static void LoadProfilesFromSourceToDestination<T>(TextAsset[] files, Dictionary<string, T> destination) where T : BaseProfile
    {
        foreach(var file in files)
        {
            var profileString = file.text;
			Debug.Log("String: " + profileString);
            T profile = new JsonReader().Read<T>(profileString) as T;
            Debug.Log("Profile: " + new JsonWriter().Write(profile));
            destination.Add(profile.Name, profile);
        }
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
