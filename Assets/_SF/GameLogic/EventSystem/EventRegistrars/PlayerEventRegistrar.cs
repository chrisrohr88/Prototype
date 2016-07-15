using UnityEngine;
using System.Collections;

namespace SF.EventSystem
{
	public class PlayerEventRegistrar : EventRegistrar
	{
		private Player _player;
		public PlayerEventRegistrar(Player player)
		{
			_player = player;
			Register();
		}

		protected override void RegisterEvents()
		{
			RegisterEvent(SFEventType.EnemyDeath, _player.EntityId);
			RegisterEvent(SFEventType.EntityHit, _player.EntityId);
		}

		protected override void RegisterEventListeners()
		{
			RegisterEventListener(SFEventType.EntityHit, new ConcreteSFEventListener<EntityHitEventData> { TargetId = _player.EntityId, MethodToExecute = _player.TakeDamage });
		}
	}
}
