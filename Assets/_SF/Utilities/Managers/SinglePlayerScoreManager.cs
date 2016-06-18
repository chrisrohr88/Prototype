using UnityEngine;
using System.Collections;
using SF.EventSystem;

public class SinglePlayerScoreManager
{
	public int Score { get; private set; }
	public event System.Action<int> OnScoreUpdated;

	public SinglePlayerScoreManager()
	{
		Score = 0;
		RegisterWithEventManager();
	}

	private void RegisterWithEventManager()
	{
		SFEventManager.RegisterEventListner(SFEventType.EnemyDeath, new ConcreteSFEventListner<EnemyDeathEventData> { MethodToExecute = HandleEnemyDeath });
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
		OnScoreUpdated.SafeInvoke(Score);
		Debug.logger.Log(Score);
	}
}
