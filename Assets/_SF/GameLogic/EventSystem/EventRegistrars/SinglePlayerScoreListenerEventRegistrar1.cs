using UnityEngine;
using System.Collections;
using SF.EventSystem;
using SF.GameLogic.EventSystem.EventData;
using SF.GameLogic.UI.Listeners;

namespace SF.GameLogic.EventSystem.EventRegistrars
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