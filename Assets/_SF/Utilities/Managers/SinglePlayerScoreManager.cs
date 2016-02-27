using UnityEngine;
using System.Collections;

public class SinglePlayerScoreManager
{
	public int Score { get; private set; }

	public SinglePlayerScoreManager()
	{
		Score = 0;
	}

	public void UpdateScore(int pointsToAward)
	{
		Score += pointsToAward;
		OnScoreUpdated();
	}

	public void OnScoreUpdated()
	{
		// TODO: Add Score UI
		Debug.logger.Log(Score);
	}
}
