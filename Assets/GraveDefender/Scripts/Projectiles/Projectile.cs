using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private float _speed = 50f;

    public static Projectile Create(Vector3 spawnPoint)
    {
        var projectile = (GameObject.Instantiate(Resources.Load("Game/Projectiles/BasicProjectile")) as GameObject).AddComponent<Projectile>();
        projectile.transform.position = spawnPoint;
        return projectile;
    }

    private void Update()
    {
        transform.localPosition += Vector3.up * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
