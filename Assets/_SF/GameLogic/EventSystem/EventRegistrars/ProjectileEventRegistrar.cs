using UnityEngine;
using System.Collections;
using SF.GameLogic.Projectiles;
using SF.EventSystem;

namespace SF.GameLogic.EventSystem.EventRegistrars
{
	public class ProjectileEventRegistrar : EventRegistrar
	{
		private Projectile _projectile;
		public ProjectileEventRegistrar(Projectile projectile)
		{
			_projectile = projectile;
			Register();
		}

		protected override void RegisterEvents()
		{
			RegisterEvent(SFEventType.ProjectileHit, _projectile.EntityId);
			RegisterEvent(SFEventType.ProjectileDestroyed, _projectile.EntityId);
		}

		protected override void RegisterEventListeners()
		{
		}
	}
}
