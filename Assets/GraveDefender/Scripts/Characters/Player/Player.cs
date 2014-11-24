using UnityEngine;
using System.Collections;

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
        Debug.Log(player.Health.TestHealth);
        player.PickupWeapon();
        return player;
    }

    private Player()
    {
    }

    public void PickupWeapon()
    {
        Weapon = Weapon.CreateFromProfile(ProfileManager.GetWeaponProfileByName("Minigun") as WeaponProfile);
    }

    ~Player()
    {
    }
}
