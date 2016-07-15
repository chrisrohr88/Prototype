using UnityEngine;
using System.Collections.Generic;
using SF.EventSystem;

namespace SF.EventSystem
{
	public class SFEvent
	{
		public SFEventType EventType { get; set; }
		public long OriginId { get; set; }
	}
}
