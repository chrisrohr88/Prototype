using UnityEngine;
using System.Collections;
using Weapons;

public abstract class AttackBehavior : CharacterBehavior
{
	protected Weapon _weapon;
	public TargetingBehavior TargetingBehavior { get; set; }
	public bool HasTarget { get; protected set; }

	protected AttackBehavior(Enemy enemy, Weapon weapon) : base(enemy)
	{
		_weapon = weapon;
	}
}