using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifiableAttributeManager : MonoBehaviour
{
    private static ModifiableAttributeManager _instance = null;
    public static ModifiableAttributeManager Instance
    {
        get
        {
            if(_instance == null)
            {
                var gameObject = new GameObject();
                gameObject.name = "ModifiableAttributes";
				_instance = gameObject.AddComponent<ModifiableAttributeManager>();
            }

            return _instance;
        }
    }
    
    private List<ModifiableAttribute> _listOfAllModifiableAttributes;

    public static bool HasInstance
    {
        get
        {
            return _instance != null;
        }
    }
    
    private void Awake()
    {
        if (_instance != null)
        {
            return;
		}
		_instance._listOfAllModifiableAttributes = new List<ModifiableAttribute>();
    }
    
    private void Update()
    {
		if (HasInstance && IsFirstModifiableAttributeExpired())
        {
            UpdateAllAttributes();
        }
    }

    private bool IsFirstModifiableAttributeExpired()
    {
        if(_listOfAllModifiableAttributes.Count > 0)
        {
            return _listOfAllModifiableAttributes[0].IsFirstModiferExpired();
        }
        return false;
    }
    
    private void UpdateAllAttributes()
    {
        foreach (var modifiableAttribute in _listOfAllModifiableAttributes)
        {
            if(modifiableAttribute.IsFirstModiferExpired())
            {
                modifiableAttribute.UpdateAttribute();
            }
        }
    }

    public void AddAttribute(ModifiableAttribute modifiableAttribute)
    {
        _listOfAllModifiableAttributes.Insert(GetInsertIndex(modifiableAttribute), modifiableAttribute);
    }

    private int GetInsertIndex(ModifiableAttribute modifiableAttributeToInsert)
    {
        int insertIndex = 0;
        foreach(var modifiableAttribute in _listOfAllModifiableAttributes)
        {
            if(ModifiableAttribute.ModifiableAttributeComparer.Compare(modifiableAttributeToInsert, modifiableAttribute) < 0)
            insertIndex++;
        }
        return insertIndex;
    }

    public void RemoveAttribute(ModifiableAttribute modifiableAttribute)
    {
        _listOfAllModifiableAttributes.Remove(modifiableAttribute);
    }
}
