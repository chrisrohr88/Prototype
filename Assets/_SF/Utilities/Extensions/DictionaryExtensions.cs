using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SF.Utilities.Extensions
{
	public static class DictionaryExtensions
	{
	    public static U SafeGetValue<T,U>(this Dictionary<T,U> dictionary, T key, U defaultValue)
	    {
			U retValue = defaultValue;
			if(dictionary.ContainsKey(key))
			{
				retValue = dictionary[key];
			}
			return retValue;
	    }
	}
}
