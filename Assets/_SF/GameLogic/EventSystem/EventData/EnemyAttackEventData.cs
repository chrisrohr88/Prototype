using System.Collections.Generic;
using UnityEngine;

namespace SF.EventSystem
{
	public class EnemyAttackEventData : SFEventData 
	{
		public Vector3 TargetPosition { get; set; }

		public EnemyAttackEventData()
		{
			EventType = SFEventType.EntityAttack;
		}
	}
}
