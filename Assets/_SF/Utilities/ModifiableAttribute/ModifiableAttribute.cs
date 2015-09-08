using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifiableAttribute
{
    #region Modifier
    public class Modifier
    {
        public float ModifyPercentage { get; private set; }
        public float TimeToExpire { get; private set; }
        public bool IsPermanent { get; private set; }

        public Modifier(float value, float duration)
        {
            ModifyPercentage = value;
            IsPermanent = (duration < 0);
            TimeToExpire = Time.time + duration;
        }

        public Modifier GetEarliestExpiringModifier(Modifier other)
        {
            if (other != null && other.TimeToExpire < this.TimeToExpire)
            {
                return other;
            }

            return this;
        }
        
        public static int Compare(Modifier a, Modifier b)
        {
            if (a == null && b == null)
            {
                return 0;
            }
            else if (a == null)
            {
                return 1;
            }
            else if (b == null)
            {
                return -1;
            }
            else
            {
                return (int)(b.TimeToExpire - a.TimeToExpire);
            }
        }
    }
    #endregion

    #region Fields, Members, and Properties
    [SerializeField] private float _baseValue = 0;
	[SerializeField] private float _modifiedValue = 0;
    private int _nextKey = 0;
    private int _nextPermanentKey = -1;
    private Modifier _firstToExpireModifier = null;
    private Dictionary<int, Modifier> _modifiers = new Dictionary<int, Modifier>();

    public float ModifiedValue
    {
        get
        {
            return _modifiedValue;
        }
    }

    public Modifier FirstToExpireModifier
    {
        get
        {
            return _firstToExpireModifier;
        }
    }

    private event System.Action<float, float> _attributeUpdated;
    public event System.Action<float, float> AttributeUpdated
    {
        add
        {
            _attributeUpdated += value;
        }
        remove
        {
            _attributeUpdated -= value;
        }
    }
    #endregion
    
    private ModifiableAttribute(float baseValue)
    {
        _baseValue = baseValue;
    }

    public static ModifiableAttribute Create(float baseValue)
    {
        var modifiableAttribute = new ModifiableAttribute(baseValue);
        ModifiableAttributeManager.Instance.AddAttribute(modifiableAttribute);
        modifiableAttribute.UpdateAttribute();
        return modifiableAttribute;
    }

    #region Add Modifiers
    public int AddModifierAndUpdateAttribute(float value, float duration = -1)
    {
        var modifier = new Modifier(value, duration);
        var key = AddModifier(modifier);
        UpdateAttribute();

        return key;
    }

    private int AddModifier(Modifier modifier)
    {
        var key = 0;

        if (modifier.IsPermanent)
        {
            key = AddPermanentModifier(modifier);
        }
        else
        {
            key = AddExpiringModifier(modifier);
        }

        return key;
    }

    private int AddPermanentModifier(Modifier modifier)
    {
        var key = _nextPermanentKey;
        _modifiers.Add(key, modifier);
        _nextPermanentKey--;
        return key;
    }

    private int AddExpiringModifier(Modifier modifier)
    {
        var key = _nextKey;
        _modifiers.Add(key, modifier);
        _nextKey++;
        return key;
    }
    #endregion

    #region Update
    public void UpdateAttribute()
    {
        var previousModifiedValue = _modifiedValue;
        RemoveExpiredModifiers();
        AssignFirstToExpireModifier();
        CalculateModifiedValue();
        _attributeUpdated.SafeInvoke(previousModifiedValue, _modifiedValue);
    }

    private void CalculateModifiedValue()
    {
        _modifiedValue = _baseValue * GetPercentModified();

    }

    private float GetPercentModified()
    {
        var totalPercentModified = 1f;

        foreach (var modifier in _modifiers.Values)
        {            
            totalPercentModified *= modifier.ModifyPercentage;
        }

        return totalPercentModified;
    }
    #endregion

    #region Remove Modifier
    public void RemoveModifierAndUpdateValue(int key)
    {
        RemoveModifier(key);
        UpdateAttribute();
    }

    private void RemoveModifier(int key)
    {
        if (_modifiers.ContainsKey(key))
        {
            _modifiers.Remove(key);
        }
    }

    private void RemoveExpiredModifiers()
    {
        var keysToRemove = FindKeysToRemove();

        foreach (var key in keysToRemove)
        {
            RemoveModifier(key);
        }
    }

    private List<int> FindKeysToRemove()
    {
        var keysToRemove = new List<int>();

        foreach (var keyValuePair in _modifiers)
        {
            if(!keyValuePair.Value.IsPermanent && keyValuePair.Value.TimeToExpire <= Time.time)
            {
                keysToRemove.Add(keyValuePair.Key);
            }
        }

        return keysToRemove;
    }
    #endregion
    
    #region Utilities
    private void AssignFirstToExpireModifier()
    {
        _firstToExpireModifier = null;
        if (_modifiers.Count > 0)
        {            
            foreach(var modifier in _modifiers.Values)
            {
                if(!modifier.IsPermanent)
                {
                    _firstToExpireModifier = modifier.GetEarliestExpiringModifier(_firstToExpireModifier);
                }
            }
        }
    }

    public void ResetAttribute()
    {
        _nextKey = 0;
        _nextPermanentKey = 0;
        _firstToExpireModifier = null;
        _modifiers.Clear();
        UpdateAttribute();
    }

    public bool IsFirstModiferExpired()
    {
        return _firstToExpireModifier != null && _firstToExpireModifier.TimeToExpire <= Time.time;
    }
    #endregion

    #region Comparer
    public static class ModifiableAttributeComparer
    {
        public static int Compare(ModifiableAttribute a, ModifiableAttribute b)
        {
            if (a == null && b == null)
            {
                return 0;
            }
            else if (a == null)
            {
                return 1;
            }
            else if (b == null)
            {
                return -1;
            }
            else
            {
                return Modifier.Compare(a.FirstToExpireModifier, b.FirstToExpireModifier);
            }
        }
    }
    #endregion

    public void Destroy()
    {
        if (ModifiableAttributeManager.HasInstance)
        {
            ModifiableAttributeManager.Instance.RemoveAttribute(this);
        }
    }
}
