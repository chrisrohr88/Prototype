using UnityEngine;
using System.Collections;

namespace SF.EventSystem
{
	public class SinglePlayerScoreManagerEventRegistrar : EventRegistrar
	{
		private SinglePlayerScoreManager _scoreManager;

		public SinglePlayerScoreManagerEventRegistrar(SinglePlayerScoreManager scoreManager)
		{
			_scoreManager = scoreManager;
			Register();
		}

		protected override void RegisterEvents()
		{
			RegisterEvent(SFEventType.SinglePlayerScoreUpdate);
		}

		protected override void RegisterEventListeners()
		{
			RegisterEventListener(SFEventType.EnemyDeath, new ConcreteSFEventListener<EnemyDeathEventData> { MethodToExecute = _scoreManager.HandleEnemyDeath });
		}
	}
}