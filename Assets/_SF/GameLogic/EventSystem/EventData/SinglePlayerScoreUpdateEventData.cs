using System.Collections.Generic;
using UnityEngine;

namespace SF.EventSystem
{
	public class SinglePlayerScoreUpdateEventData : SFEventData 
	{
		public int NewPointValue { get; set; }

		public SinglePlayerScoreUpdateEventData()
		{
			EventType = SFEventType.SinglePlayerScoreUpdate;
		}
	}
}
