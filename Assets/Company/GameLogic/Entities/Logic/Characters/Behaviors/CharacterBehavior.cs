using UnityEngine;
using System.Collections;
using Weapons;


public abstract class CharacterBehavior
{
	protected BaseEnemy _gameObject;
	public CharacterBehavior NextBehavior { get; protected set; }
	
	protected abstract void StartBehavior();
	public abstract void UpdateBehavior();
	protected abstract void FinishBehavior();
}