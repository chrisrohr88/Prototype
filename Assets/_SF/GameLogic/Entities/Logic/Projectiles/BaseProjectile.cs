using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour
{
	public Projectile Projectile { get; set; }

    protected void OnDestroy()
	{
    }

	protected virtual void Update()
	{
		transform.localPosition += Projectile.Velocity * Projectile.Speed * Time.deltaTime;
	}
}
