using UnityEngine;
using System.Collections;
using SF.GameLogic.Data.Enums;

namespace SF.Utilities
{
	public class DamageData
	{
	    public long AttackerId { get; set; }
	    public long? TargetId { get; set; }
	    public float Damage { get; set; }
	    public DamageType DamageType { get; set; }
	}
}
