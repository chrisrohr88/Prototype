using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float _damage = 100f;
    [SerializeField] protected GameObject _deathEffectPrefab;

    protected HealthComponent _health = null;

    public bool IsPressed { get; protected set; }

    public float Damage
    {
        get
        {
            return _damage;
        }

        protected set
        {
            _damage = value;
        }
    }

    protected abstract void PreStart();
    protected abstract void Start();
    protected abstract void PostStart();

    protected abstract void Update();
    protected abstract void FixedUpdate();
    
    protected void Awake()
    {
        _health = HealthComponent.Create(250);
        _health.Death += OnDeath;
    }

    protected void UpdateHealth(float amount)
    {
        _health.UpdateHealth(amount);
    }

    protected void OnDeath()
    {
        var go = Instantiate(_deathEffectPrefab) as GameObject;
        go.transform.position = transform.position - new Vector3(0,0, 1);
        Destroy(gameObject, .1f);
    }

    protected void OnDestroy()
    {
        _health.Destroy();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        var damageData = other.gameObject.GetComponent<DamageData>();
        if (damageData != null)
        {
            UpdateHealth(-damageData.Damage);
        }
    }
}
