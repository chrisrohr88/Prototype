using System.Collections.Generic;

namespace SF.EventSystem
{
	public class EnemyDeathEventData : SFEventData 
	{
		public int PointValue { get; set; }

		public EnemyDeathEventData()
		{
			EventType = SFEventType.EnemySpawned;
		}
	}
}
