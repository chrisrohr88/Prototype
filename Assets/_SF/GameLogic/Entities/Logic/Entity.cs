using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FlakeGen;
using SF.EventSystem;
using SF.GameLogic.Data.Enums;

namespace SF.GameLogic.Entities.Logic
{
	public abstract class Entity
	{
		private static Id64Generator ID_GENERATOR = new Id64Generator((int)GeneratorIds.Entity);

		protected EventRegistrar _eventRegistar;

		public long EntityId { get; private set; }

		public Entity()
		{
			EntityId = ID_GENERATOR.GenerateId();
			Debug.Log(EntityId);
		}

		protected virtual void OnDeath()
		{
			_eventRegistar.Unregister();
		}
	}
}
