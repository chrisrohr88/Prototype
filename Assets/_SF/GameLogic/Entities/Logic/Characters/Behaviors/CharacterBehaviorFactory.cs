﻿using UnityEngine;
using System.Collections;
using Weapons;
using System;

public static class CharacterBehaviorFactory
{
	public static MovementBehavior CreateMovementBehaviorFromType(MovementBehaviorType movementType, Enemy enemy, System.Action callback = null)
	{
		switch(movementType)
		{
			case MovementBehaviorType.BasicMovement:
				return new BasicMovementBehavior(enemy);
			case MovementBehaviorType.Stagger:
			return new StaggerMovementBehavior(enemy, 1, callback); // TODO: set time
			case MovementBehaviorType.Blink:
				return new BlinkMovementBehavior(enemy, 1, callback); // TODO: set time
		}
		throw new Exception("MovementBehaviorType: " + movementType.ToString() + " has not been implemented");
	}

	public static AttackBehavior CreateAttackBehaviorFromType(AttackBehaviorType attackType, Enemy enemy, Weapon weapon)
	{
		switch(attackType)
		{
			case AttackBehaviorType.BasicAttack:
				return new BasicAttackBehavior(enemy, weapon);
		}
		throw new Exception("AttackBehaviorType: " + attackType.ToString() + " has not been implemented");
	}

	public static TargetingBehavior CreateTargetingBehaviorFromType(TargetingBehaviorType targetingType, Enemy enemy)
	{
		switch(targetingType)
		{
			case TargetingBehaviorType.SimpleTargeting:
				return new SimpleTagetingBehavior(enemy);
		}
		throw new Exception("TargetingBehaviorType: " + targetingType.ToString() + " has not been implemented");
	}
}