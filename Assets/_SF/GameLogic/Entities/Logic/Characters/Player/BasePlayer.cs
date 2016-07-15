using UnityEngine;
using System.Collections;

namespace SF.GameLogic.Entities.Logic.Charaters.Player
{
	public class BasePlayer : MonoBehaviour
	{
		public Player Player { get; set; }

	    protected virtual void Start()
		{
			Player.Health.Death += OnDeath;
		}

	    protected void OnDeath()
	    {
	        Destroy(gameObject);
	    }

	    protected void OnDestroy()
		{
			Player.Health.Death -= OnDeath;
	    }
	}
}
