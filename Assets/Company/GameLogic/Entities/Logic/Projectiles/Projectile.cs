using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 100;
	private Vector3 _velocity;

	public static Projectile Create(Transform spawnTransform)
    {
		var projectile = (GameObject.Instantiate(Resources.Load("Game/Projectiles/BasicProjectile"), spawnTransform.position, Quaternion.identity) as GameObject).AddComponent<Projectile>();
		projectile._velocity = spawnTransform.forward;
        return projectile;
    }

    private void Update()
    {
		transform.localPosition += _velocity * _speed * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
