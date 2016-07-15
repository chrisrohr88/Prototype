using UnityEngine;
using System.Collections;
using SF.GameLogic.Entities.Logic.Charaters.Player;
using SF.EventSystem;
using SF.GameLogic.EventSystem.EventData;

namespace SF.GameLogic.EventSystem.EventRegistrars
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
			RegisterEvent(SFEventType.PlayerDeath, _player.EntityId);
			RegisterEvent(SFEventType.EntityHit, _player.EntityId);
		}

		protected override void RegisterEventListeners()
		{
			RegisterEventListener(SFEventType.EntityHit, new ConcreteSFEventListener<EntityHitEventData> { TargetId = _player.EntityId, MethodToExecute = _player.TakeDamage });
		}
	}
}
