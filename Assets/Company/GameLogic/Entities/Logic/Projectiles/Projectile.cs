using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 100;

    public static Projectile Create(Transform spawnTransform)
    {
		var projectile = (GameObject.Instantiate(Resources.Load("Game/Projectiles/BasicProjectile"), spawnTransform.position, spawnTransform.rotation) as GameObject).AddComponent<Projectile>();
        return projectile;
    }

    private void Update()
    {
        transform.localPosition += Vector3.right * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
