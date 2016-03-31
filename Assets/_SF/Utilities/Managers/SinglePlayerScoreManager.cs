using UnityEngine;
using System.Collections;

public class SinglePlayerScoreManager
{
	public int Score { get; private set; }
	public event System.Action<int> OnScoreUpdated;

	public SinglePlayerScoreManager()
	{
		Score = 0;
	}

	public void UpdateScore(int pointsToAward)
	{
		Score += pointsToAward;
		ScoreUpdated();
	}

	public void ScoreUpdated()
	{
		OnScoreUpdated.SafeInvoke(Score);
		Debug.logger.Log(Score);
	}
}
