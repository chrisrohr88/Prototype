﻿using System.Collections;
using UnityEngine;
using Weapons;

public class Player
{
    public Weapon Weapon { get; private set; }
	public HealthComponent Health { get; private set; }
	public int id = 1;

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
		SubscribeEvents();
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
		Weapon = WeaponFactory.CreateFromProfile(profile, GameMode.FireTransform);
		Weapon.EntityId = id;
		Debug.Log("Weapon is " + Weapon.Name);
	} 

	public void TriggerPulled(Vector3 position)
	{
		Weapon.TriggerPulled(position);
	}
	
	public void TriggerHeld(Vector3 position)
	{
		Weapon.TriggerHeld(position);
	}
	
	public void TriggerReleased(Vector3 position)
	{
		Weapon.TriggerReleased(position);
	}
	
	private void SubscribeEvents()
	{
		UnsubscribeEvents();
		FieldInteractable.OnHeld += TriggerHeld;
		FieldInteractable.OnMoved += TriggerHeld;
		FieldInteractable.OnPressed += TriggerPulled;
		FieldInteractable.OnReleased += TriggerReleased;
	}
	
	public void UnsubscribeEvents()
	{
		FieldInteractable.OnHeld -= TriggerHeld;
		FieldInteractable.OnMoved -= TriggerHeld;
		FieldInteractable.OnPressed -= TriggerPulled;
		FieldInteractable.OnReleased -= TriggerReleased;
	}

    ~Player()
	{
		UnsubscribeEvents();
	}
}
