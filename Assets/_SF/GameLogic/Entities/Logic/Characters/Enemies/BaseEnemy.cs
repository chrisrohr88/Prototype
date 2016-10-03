using UnityEngine;
using System.Collections;
using SF.CustomInspector.Attributes;

namespace SF.GameLogic.Entities.Logic.Charaters.Enemies
{
	public class BaseEnemy : MonoBehaviour
	{
		[SerializeField][InspectorValue(Label = "Death Prefab")] protected GameObject _deathEffectPrefab;
		[SerializeField][InspectorValue] protected Transform _spawnTransform;

		[InspectorObject(Label = "Enemy")] public Enemy Enemy { get; set; }
	    
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
}
