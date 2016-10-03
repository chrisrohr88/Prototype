using UnityEngine;
using System.Collections;
using SF.Utilities.Extensions;
using SF.Utilities.ModifiableAttributes;
using SF.CustomInspector.Attributes;

namespace SF.GameLogic.Entities.Logic.Components
{
	public class HealthComponent
	{
		[InspectorObject] private ModifiableAttribute _maxHealthAttribute = null;
		[InspectorValue] private float _currentHealth;
		[InspectorValue(Options = OptionType.ReadOnly)] private bool _isDead;

	    public float TestHealth
	    {
	        get
	        {
	            return _currentHealth;
	        }
	    }

	    public float MaxHealth
	    {
	        get
	        {
	            return _maxHealthAttribute.ModifiedValue;
	        }
	    }

	    public bool IsDead
	    {
	        get
	        {
	            return _isDead;
	        }
	    }

	    public ModifiableAttribute MaxHealthAttribute
	    {
	        get
	        {
	            return _maxHealthAttribute;
	        }
	    }

	    #region Events
	    private event System.Action<float> _healthUpdated;
	    public event System.Action<float> HealthUpdated
	    {
	        add
	        {
	            _healthUpdated += value;
	        }
	        remove
	        {
	            _healthUpdated -= value;
	        }
	    }
	    
	    private event System.Action _death;
	    public event System.Action Death
	    {
	        add
	        {
	            _death += value;
	        }
	        remove
	        {
	            _death -= value;
	        }
	    }
	    #endregion

	    private HealthComponent()
	    {
	    }

	    public static HealthComponent Create(float baseHealth)
	    {
	        var healthComponent = new HealthComponent();
	        healthComponent.Initialize(baseHealth);
	        return healthComponent;
	    }

	    private void Initialize(float baseHealth)
	    {
	        _maxHealthAttribute = ModifiableAttribute.Create(baseHealth);
	        _maxHealthAttribute.AttributeUpdated += OnMaxHealthMoodified;
	        _currentHealth = MaxHealth;
	    }

	    #region Update Health
	    public void UpdateHealth(float amount)
	    {
	        if (!_isDead)
	        {
	            AddToCurrentHealth(amount);
	            _healthUpdated.SafeInvoke<float>(_currentHealth);
	            CheckIsDead();
	        }
	    }

	    private void AddToCurrentHealth(float amount)
	    {
	        var healthAfterUpdate = _currentHealth + amount;
	        _currentHealth = (healthAfterUpdate > MaxHealth) ? MaxHealth : healthAfterUpdate;
	    }
	    #endregion

	    #region EventHandlers
	    private void OnMaxHealthMoodified(float previousMax, float currentMax)
	    {
	        if (!IsDead && (_currentHealth > currentMax))
	        {
	            var percentOfChange = Mathf.Abs(currentMax / previousMax);

	            _currentHealth *= percentOfChange;

	            _healthUpdated.SafeInvoke(_currentHealth);
	        }
	    }
	    #endregion

	    #region Utilities
	    private void CheckIsDead()
	    {  
	        if(_currentHealth <= 0)
	        {
	            SetAsDead();
	        }
	    }

	    private void SetAsDead()
	    {
	        _currentHealth = 0;
	        _isDead = true;;
	        _death.SafeInvoke();
	    }
	    #endregion

	    public void Destroy()
	    {
	        _maxHealthAttribute.Destroy();
	        _maxHealthAttribute.AttributeUpdated -= OnMaxHealthMoodified;
	    }
	}
}
