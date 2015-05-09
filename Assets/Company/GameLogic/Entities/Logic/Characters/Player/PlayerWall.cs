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
        if(other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<BaseEnemy>();
            _player.Health.UpdateHealth(-enemy.Enemy.Damage);
            Debug.Log("Health: " + _player.Health.TestHealth);
        }
    }
}
