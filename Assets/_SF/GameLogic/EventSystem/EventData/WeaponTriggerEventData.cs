using UnityEngine;

namespace SF.EventSystem
{
	public class WeaponTriggerEventData : SFEventData 
	{
		public Vector3 TargetPosition { get; set; }

		public WeaponTriggerEventData()
		{
		}
	}
}
