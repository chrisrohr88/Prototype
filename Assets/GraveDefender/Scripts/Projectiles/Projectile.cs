using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private float _speed = 50f;

    public static Projectile Create(Transform spawnPoint)
    {
        var projectile = (GameObject.Instantiate(Resources.Load("Game/Projectiles/BasicProjectile")) as GameObject).AddComponent<Projectile>();
        projectile.transform.position = spawnPoint.position;
        return projectile;
    }

    private void Update()
    {
        transform.localPosition += Vector3.up * _speed * Time.deltaTime;
    }
}
