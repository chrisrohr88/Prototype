using System.Collections.Generic;
using UnityEngine;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventData
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
