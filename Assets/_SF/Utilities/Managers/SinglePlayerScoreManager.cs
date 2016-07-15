using UnityEngine;
using System.Collections;
using SF.EventSystem;
using System.Collections.Generic;

public class SinglePlayerScoreManager
{
	private EventRegistrar _eventRegistar;
	public int Score { get; private set; }

	public SinglePlayerScoreManager()
	{
		Score = 0;
		_eventRegistar = new SinglePlayerScoreManagerEventRegistrar(this);
	}

	public void HandleEnemyDeath(EnemyDeathEventData eventData)
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
}
