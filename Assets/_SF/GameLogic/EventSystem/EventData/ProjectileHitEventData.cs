using UnityEngine;
using System.Collections;

namespace SF.EventSystem
{
	public class ProjectileHitEventData : SFEventData
	{
		public long? HitObjectId { get; set; }
		public GameObject GameObjectHit { get; set; }

		public ProjectileHitEventData()
		{
			EventType = SFEventType.ProjectileHit;
		}
	}
}

