using System.Collections.Generic;

namespace SF.EventSystem
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
