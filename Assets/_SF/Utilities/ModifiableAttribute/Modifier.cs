using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SF.Utilities.ModifiableAttributes
{
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
}
