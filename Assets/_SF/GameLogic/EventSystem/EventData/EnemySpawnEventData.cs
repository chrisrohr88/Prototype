using System.Collections.Generic;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventData
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
