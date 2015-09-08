using UnityEngine;
using System.Collections;

public class PlayerWall : MonoBehaviour
{
    private Player _player;

    public void AssignPlayer(Player player)
    {
        _player = player;
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
}
