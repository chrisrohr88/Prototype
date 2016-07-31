using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using SF.EventSystem;
using SF.GameLogic.Controls.Interactables;
using SF.GameLogic.Data.Profiles;
using SF.GameLogic.Entities.Logic.Charaters.Player;
using SF.GameLogic.Entities.Logic.Spawner;
using SF.Utilities.Managers;

namespace SF.GameLogic.GameModes
{
	public class GameMode
	{
		public SinglePlayerScoreManager ScoreManager { get; private set; }

		//TODO Do this better
	    private FieldInteractable _field;
		private Player _player;

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
			SFEventManager.FireEvent(new SFEventData { OriginId = SFEventManager.SYSTEM_ORIGIN_ID, EventType = SFEventType.LevelStart } );
		}

	    private void LoadGameModeDependancies()
		{
			ProfileManager.LoadProfiles(null);
			SpawnManager.LoadLevelEnemies (new List<EnemyProfile> {ProfileManager.GetEnemyProfile("Skeleton")});
			ScoreManager = new SinglePlayerScoreManager();

			InstantiateLevelObjects();
	    }

		private void InstantiateLevelObjects()
		{
			_field = (GameManager.Instantiate(Resources.Load("Game/Field/Field")) as GameObject).GetComponent<FieldInteractable>();
			GameManager.Instantiate(Resources.Load("InputManager"));
			var playerWall = (GameManager.Instantiate(Resources.Load("Game/Field/Barrier")) as GameObject).GetComponent<PlayerWall>();
			_player = Player.Create(1000);

			// TODO: Save these for tracking purposes
			new EntitySpawner();
			playerWall.AssignPlayer(_player);
			playerWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 100)) + new Vector3(-80, 4, 0);
			SetupEventRegistar();
		}

		private void SetupEventRegistar()
		{
			SFEventManager.RegisterEventListener(SFEventType.PlayerDeath, new ConcreteSFEventListener<SFEventData> { MethodToExecute = OnPlayerDeath } ); 
			SFEventManager.RegisterEventListener(SFEventType.GameOver, new ConcreteSFEventListener<SFEventData> { MethodToExecute = EndLevel } ); 
		}

		private void OnPlayerDeath(SFEventData eventData)
		{
			GameManager.Instance.EndGame();
		}

		public void EndLevel(SFEventData eventData)
		{
			SceneManager.LoadScene("Main Menu");
		}

		public void UnloadLevel()
		{
		}
	}
}
