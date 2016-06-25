using UnityEngine;
using System.Collections.Generic;

namespace SF.EventSystem
{
	public class SFEvent
	{
		public SFEventType EventType { get; set; }
		public long OriginId { get; set; }
	}
}
