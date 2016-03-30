﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameModeType
{
	Survival
}

public class GameMode
{
	public SinglePlayerScoreManager ScoreManager { get; private set; }

	//TODO Do this better
    private FieldInteractable _field;

    public Transform FireTransform
    {
        get
        {
            return _field.FireTransform;
        }
    }

	public void StartGame()
    {
        LoadGameModeDependancies();
		StartLevel();
	}

    private void LoadGameModeDependancies()
	{
		// TODO: Load Blocker object???
		EnemyManager.LoadEnemies (new List<EnemyProfile> {ProfileManager.GetEnemyProfile("Skeleton")});
		ScoreManager = new SinglePlayerScoreManager();

		InstantiateLevelObjects();
    }

	private void InstantiateLevelObjects()
	{
		_field = (GameManager.Instantiate(Resources.Load("Game/Field/Field")) as GameObject).GetComponent<FieldInteractable>();
		GameManager.Instantiate(Resources.Load("InputManager"));
		var playerWall = (GameManager.Instantiate(Resources.Load("Game/Field/Barrier")) as GameObject).GetComponent<PlayerWall>();
		playerWall.AssignPlayer(Player.Create(1000));
		playerWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 100)) + new Vector3(-80, 4, 0);
	}

	public void StartLevel()
	{
		EnemyManager.EnableSpawning();
		// TODO: unLoad Blocker object???
	}
}
