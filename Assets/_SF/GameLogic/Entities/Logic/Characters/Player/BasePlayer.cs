using UnityEngine;
using System.Collections;

public class BasePlayer : MonoBehaviour
{
	public Player Player { get; set; }

    protected virtual void Start()
	{
		Player.Health.Death += OnDeath;
	}

    protected void OnDeath()
    {
        Destroy(gameObject, .1f);
    }

    protected void OnDestroy()
	{
		Player.Health.Death -= OnDeath;
    }
}
