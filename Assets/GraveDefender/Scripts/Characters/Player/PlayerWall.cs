using UnityEngine;
using System.Collections;

public class PlayerWall : MonoBehaviour
{
    private Player _player;

    public void AssignPlayer(Player player)
    {
        _player = player;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("WHY");
        if(other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<BaseEnemy>();
            _player.Health.UpdateHealth(-enemy.Damage);
            Debug.Log("Health: " + _player.Health.TestHealth);
        }
    }
}
