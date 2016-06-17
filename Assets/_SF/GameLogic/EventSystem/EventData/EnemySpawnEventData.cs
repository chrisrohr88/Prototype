using System.Collections.Generic;

namespace SF.EventSystem
{
	public class EnemySpawnEventData : SFEventData 
	{
		public long EnemyId { get; set; }

		public EnemySpawnEventData()
		{
			OriginId = SFEventManager.SYSTEM_ORIGIN_ID;
			EventType = SFEventType.EnemySpawned;
		}
	}
}
