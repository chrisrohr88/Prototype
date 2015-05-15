using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour
{
	[SerializeField] protected GameObject _deathEffectPrefab;

    protected abstract void PreStart();
    protected abstract void PostStart();
	
	public Transform SpwanTransform;
	public Enemy Enemy { get; set; }
    
    protected virtual void Start()
    {
		Enemy.Health.Death += OnDeath;
	}
	
	protected virtual void Update()
	{
		Enemy.Update();
	}

    protected void OnDeath()
    {
        var go = Instantiate(_deathEffectPrefab) as GameObject;
        go.transform.position = transform.position - new Vector3(0,0, 1);
        Destroy(gameObject, .1f);
    }

    protected void OnDestroy()
	{
		Enemy.Health.Death -= OnDeath;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        var damageData = other.gameObject.GetComponent<DamageData>();

        if (damageData != null)
        {
			Enemy.TakeDamage(damageData);
			Destroy (other.gameObject);
        }
    }
}
