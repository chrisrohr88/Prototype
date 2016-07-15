using UnityEngine;
using System.Collections;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.EventSystem;
using SF.GameLogic.EventSystem.EventData;

namespace SF.GameLogic.EventSystem.EventRegistrars
{
	public class EnemyEventRegistrar : EventRegistrar
	{
		private Enemy _enemy;
		public EnemyEventRegistrar(Enemy enemy)
		{
			_enemy = enemy;
			Register();
		}

		protected override void RegisterEvents()
		{
			RegisterEvent(SFEventType.EnemyDeath, _enemy.EntityId);
			RegisterEvent(SFEventType.EntityHit, _enemy.EntityId);
			RegisterEvent(SFEventType.EntityAttack, _enemy.EntityId);
		}

		protected override void RegisterEventListeners()
		{
			RegisterEventListener(SFEventType.EntityHit, new ConcreteSFEventListener<EntityHitEventData> { TargetId = _enemy.EntityId, MethodToExecute = _enemy.TakeDamage });
		}
	}
}
