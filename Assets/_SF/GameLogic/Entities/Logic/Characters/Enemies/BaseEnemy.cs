using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour
{
	[SerializeField] protected GameObject _deathEffectPrefab;
	[SerializeField] protected Transform _spawnTransform;

	public Enemy Enemy { get; set; }
    
	public Transform SpawnTransform
	{
		get
		{
			return _spawnTransform;
		}
	}

    protected virtual void Start()
	{
		Enemy.Health.Death += OnDeath;
	}
	
	protected virtual void Update()
	{
		if(Enemy != null)
		{
			Enemy.Update();
		}
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
}
