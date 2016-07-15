using UnityEngine;
using System.Collections;

public class PlayerWall : MonoBehaviour
{
	public Player Player { get; private set; }

    public void AssignPlayer(Player player)
    {
        Player = player;
		Player.Health.Death += OnPlayerDeath;
    }

	private void OnPlayerDeath()
	{
		Destroy(this.gameObject);
		Player.UnsubscribeEvents();
		GameManager.Instance.EndGame();
	}

	private void OnDestroy()
	{
		Player.Health.Death -= OnPlayerDeath;
	}
}
