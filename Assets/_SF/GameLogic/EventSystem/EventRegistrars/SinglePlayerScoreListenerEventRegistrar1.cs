using UnityEngine;
using System.Collections;

namespace SF.EventSystem
{
	public class SinglePlayerScoreListenerEventRegistrar : EventRegistrar
	{
		private ScoreListener _scoreListener;

		public SinglePlayerScoreListenerEventRegistrar(ScoreListener scoreListener)
		{
			_scoreListener = scoreListener;
			Register();
		}

		protected override void RegisterEvents()
		{
		}

		protected override void RegisterEventListeners()
		{
			RegisterEventListener(SFEventType.SinglePlayerScoreUpdate, new ConcreteSFEventListener<SinglePlayerScoreUpdateEventData> { MethodToExecute = _scoreListener.UpdateScore });
		}
	}
}