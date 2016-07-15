using UnityEngine;
using System.Collections;
using SF.Utilities.Managers;

namespace SF.GameLogic.Entities.Logic.Charaters.Player
{
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
		}

		private void OnDestroy()
		{
			Player.Health.Death -= OnPlayerDeath;
		}
	}
}
