using System.Collections.Generic;

namespace SF.EventSystem
{
	public class EnemyHitEventData : SFEventData 
	{
		public DamageData DamageData { get; set; }

		public EnemyHitEventData()
		{
			EventType = SFEventType.EnemyHit;
		}
	}
}
