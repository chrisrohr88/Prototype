using System.Collections.Generic;
using SF.EventSystem;
using SF.GameLogic.Data.Profiles;
using SF.GameLogic.Entities.Logic.Charaters.Behaviors;
using SF.GameLogic.Entities.Logic.Charaters.Behaviors.Attack;
using SF.GameLogic.Entities.Logic.Charaters.Behaviors.Movement;
using SF.GameLogic.Entities.Logic.Weapons.Controllers;
using UnityEngine;
using SF.GameLogic.Entities.Logic.Weapons;
using SF.GameLogic.Entities.Logic.Components;
using SF.GameLogic.EventSystem.EventData;
using SF.GameLogic.EventSystem.EventRegistrars;
using SF.CustomInspector.Attributes;

namespace SF.GameLogic.Entities.Logic.Charaters.Enemies
{
	public class Enemy : Character
	{
		[InspectorObject] public Weapon Weapon { get; set; }
		public BaseEnemy EnemyRenderable { get; set; }
		[InspectorValue] public float Speed { get; set; }
		public CharacterBehavior MovementBehavior { get; set; }
		public AttackBehavior AttackBehavior { get; set; }
		public LayerMask TargetingLayerMask { get; private set; }
		[InspectorValue] public int PointValue { get; private set; }
		public WeaponController WeaponController { get; private set; }

		public static Enemy Create(EnemyProfile profile, Weapon weapon, BaseEnemy baseEnemy)
		{
			var enemy = new Enemy();
			enemy._eventRegistar = new EnemyEventRegistrar(enemy);
			enemy.MovementBehavior = CharacterBehaviorFactory.CreateMovementBehaviorFromType(profile.MovementBehaviorType, enemy);
			enemy.Speed = profile.Speed;
			enemy.TargetingLayerMask = (LayerMask) profile.LayerMask;
			enemy.PointValue = profile.PointValue;
			enemy.Health = HealthComponent.Create(profile.BaseHealth);
			enemy.Health.Death += enemy.OnDeath;

			if(weapon != null)
			{
				enemy.AttackBehavior = CharacterBehaviorFactory.CreateAttackBehaviorFromType(profile.AttackBehaviorType, enemy, weapon);
				enemy.AttackBehavior.TargetingBehavior = CharacterBehaviorFactory.CreateTargetingBehaviorFromType(profile.TargetingBehaviorType, enemy);
				enemy.Weapon = weapon;
				enemy.Weapon.PlayerEntityId = enemy.EntityId;
			}

			if(baseEnemy != null)
			{
				enemy.EnemyRenderable = baseEnemy;
				enemy.EnemyRenderable.Enemy = enemy;
			}

			enemy.WeaponController = new WeaponController(enemy.Weapon);
			return enemy;
		}

		private Enemy() : base()
		{
		}

		protected override void OnDeath()
		{
			SFEventManager.FireEvent(
				new EnemyDeathEventData 
				{ 
					OriginId = this.EntityId, 
					EventType = SFEventType.EnemyDeath, 
					PointValue = this.PointValue,
					TargetId = this.EntityId
				});
			Health.Death -= OnDeath;
			base.OnDeath();
		}

		public void Update()
		{
			AttackBehavior.UpdateBehavior();
			if(!AttackBehavior.HasTarget)
			{
				MovementBehavior.UpdateBehavior();
			}
		}

		protected void OnMovementBehaviorComplete()
		{
			MovementBehavior = new BasicMovementBehavior(this);
		}
		
		public void UpdateHealth(float amount)
		{
			Health.UpdateHealth(amount);
		}

		public void TakeDamage(EntityHitEventData eventData)
		{
			if(eventData.DamageData.AttackerId != EntityId)
			{
				UpdateHealth(-eventData.DamageData.Damage);
			}
		}
	}
}
