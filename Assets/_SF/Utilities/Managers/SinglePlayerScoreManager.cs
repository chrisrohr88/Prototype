using UnityEngine;
using System.Collections;
using SF.EventSystem;
using System.Collections.Generic;

public class SinglePlayerScoreManager
{
	private EventRegistar _eventRegistar;
	public int Score { get; private set; }

	public SinglePlayerScoreManager()
	{
		Score = 0;
		SetupEventRegistar();
	}

	// TODO : want to make this datadriven & move to Entity
	private	List<SFEvent> CreateEventsToRegister()
	{
		return new List<SFEvent>
		{
			new SFEvent { EventType = SFEventType.SinglePlayerScoreUpdate, OriginId = SFEventManager.SYSTEM_ORIGIN_ID }
		};
	}

	private void SetupEventRegistar()
	{
		_eventRegistar = new EventRegistar(CreateEventsToRegister());
		_eventRegistar.RegisterEvents();
		SFEventManager.RegisterEventListner(SFEventType.EnemyDeath, new ConcreteSFEventListner<EnemyDeathEventData> { MethodToExecute = HandleEnemyDeath });
		SFEventManager.RegisterEventListner(SFEventType.GameOver, new ConcreteSFEventListner<SFEventData> { MethodToExecute = HandleGameOver });
	}

	private void HandleEnemyDeath(EnemyDeathEventData eventData)
	{
		UpdateScore(eventData.PointValue);
	}

	private void UpdateScore(int pointsToAward)
	{
		Score += pointsToAward;
		ScoreUpdated();
	}

	private void ScoreUpdated()
	{
		SFEventManager.FireEvent(new SinglePlayerScoreUpdateEventData { OriginId = SFEventManager.SYSTEM_ORIGIN_ID, EventType = SFEventType.SinglePlayerScoreUpdate, NewPointValue = Score });
		Debug.logger.Log(Score);
	}

	private void HandleGameOver(SFEventData eventData)
	{
		_eventRegistar.UnregisterAllEvents();
	}
}
