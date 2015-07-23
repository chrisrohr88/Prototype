using UnityEngine;
using System.Collections;
using Weapons;

public abstract class CharacterBehavior
{
	protected Enemy _enemy;

	protected CharacterBehavior(Enemy enemy)
	{
		_enemy = enemy;
	}

	protected abstract void StartBehavior();
	public abstract void UpdateBehavior();
	protected abstract void FinishBehavior();
}