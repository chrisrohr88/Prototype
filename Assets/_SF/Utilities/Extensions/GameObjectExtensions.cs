using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SF.Utilities.Extensions
{
	public static class GameObjectExtensions
	{
	    public static T[] GetComponentsIncludingInterface<T>(this GameObject gameObject) where T : class
	    {
	        List<T> listWithInterfaces = null;

	        var components = gameObject.GetComponents<Component>();

	        foreach (var component in components)
	        {
	            if(component is T)
	            {
	                listWithInterfaces = listWithInterfaces ?? new List<T>();

	                listWithInterfaces.Add(component as T);
	            }
	        }

	        return (listWithInterfaces == null) ? null : listWithInterfaces.ToArray();
	    }

	    public static T GetComponentIncludingInterface<T>(this GameObject gameObject) where T : class
	    {
	        var list = gameObject.GetComponentsIncludingInterface<T>();

	        if (list != null && list.Length > 0)
	        {
	            return list[0];
	        }

	        return null;
	    }
	}
}
