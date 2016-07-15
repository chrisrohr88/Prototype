using System.Collections.Generic;
using SF.EventSystem;
using SF.Utilities;

namespace SF.GameLogic.EventSystem.EventData
{
	public class EntityHitEventData : SFEventData 
	{
		public DamageData DamageData { get; set; }

		public EntityHitEventData()
		{
			EventType = SFEventType.EntityHit;
		}
	}
}
