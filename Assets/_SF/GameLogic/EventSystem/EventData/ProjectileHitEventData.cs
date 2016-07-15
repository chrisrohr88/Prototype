using UnityEngine;
using System.Collections;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventData
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

