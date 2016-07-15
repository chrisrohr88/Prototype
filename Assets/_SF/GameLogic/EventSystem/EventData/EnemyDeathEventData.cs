using System.Collections.Generic;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventData
{
	public class EnemyDeathEventData : SFEventData 
	{
		public int PointValue { get; set; }

		public EnemyDeathEventData()
		{
			EventType = SFEventType.EnemyDeath;
		}
	}
}
