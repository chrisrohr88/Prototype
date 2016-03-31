using UnityEngine;
using System.Collections;

public class PlayerWall : MonoBehaviour
{
    private Player _player;

    public void AssignPlayer(Player player)
    {
        _player = player;
		_player.Health.Death += OnPlayerDeath;
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
		var damageData = other.gameObject.GetComponent<DamageData>();

		if(damageData != null && damageData.AttackerId != _player.id)
        {
			_player.Health.UpdateHealth(-damageData.Damage);
			Destroy (other.gameObject);
        }
    }

	private void OnPlayerDeath()
	{
		Destroy(this.gameObject);
		_player.UnsubscribeEvents();
		GameManager.Instance.EndGame();
	}

	private void OnDestroy()
	{
		_player.Health.Death -= OnPlayerDeath;
	}
}
