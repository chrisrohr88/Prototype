using UnityEngine;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventData
{
	public class WeaponTriggerEventData : SFEventData 
	{
		public Vector3 TargetPosition { get; set; }

		public WeaponTriggerEventData()
		{
		}
	}
}
