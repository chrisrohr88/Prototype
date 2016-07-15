using System.Collections.Generic;

namespace SF.EventSystem
{
	public class SFEventData 
	{
		public SFEventType EventType { get; set; }
		public long OriginId { get; set; }
		public long? TargetId { get; set; }
	}
}
