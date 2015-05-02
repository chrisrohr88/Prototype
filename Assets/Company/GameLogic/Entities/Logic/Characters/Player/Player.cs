using System.Collections;
using UnityEngine;
using Weapons;

public class Player
{
    public Weapon Weapon { get; private set; }
    public HealthComponent Health { get; private set; }

    public event System.Action Death
    {
        add
        {
            Health.Death += value;
        }
        remove
        {
            Health.Death -= value;
        }
    }

    public static Player Create(float baseHealth)
    {
        var player = new Player();
		player.Health = HealthComponent.Create(baseHealth);
		player.Death += () => { Debug.Log ("Player is dead!"); };
        player.PickupWeapon();
        return player;
    }

    private Player()
    {
    }

	public void PickupWeapon(string profileName)
	{
		PickupWeapon(ProfileManager.GetWeaponProfileByName(profileName));
	}

    public void PickupWeapon()
    {
        PickupWeapon(ProfileManager.GetRandomWeapon());
	}
	
	public void PickupWeapon(WeaponProfile profile)
	{
		if(Weapon != null)
		{
			Weapon.UnsubscribeEvents();
		}
		Weapon = WeaponFactory.CreateFromProfile(profile);
		Debug.Log("Weapon is " + Weapon.Name);
	}

    ~Player()
    {
    }
}
