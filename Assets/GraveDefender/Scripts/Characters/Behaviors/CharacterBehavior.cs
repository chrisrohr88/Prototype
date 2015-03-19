﻿using UnityEngine;
using System.Collections;

public abstract class CharacterBehavior
{
	protected GameObject _gameObject;
	protected CharacterBehavior _nextBehavior;
	
	protected abstract void StartBehavior();
	public abstract void UpdateGameObject();
	protected abstract void FinishBehavior();
}
