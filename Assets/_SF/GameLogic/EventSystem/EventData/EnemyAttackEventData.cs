using System.Collections.Generic;
using UnityEngine;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventData
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
