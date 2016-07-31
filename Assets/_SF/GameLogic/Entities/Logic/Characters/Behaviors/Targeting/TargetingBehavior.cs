using UnityEngine;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.CustomInspector.Attributes;

namespace SF.GameLogic.Entities.Logic.Charaters.Behaviors.Targeting
{
	public abstract class TargetingBehavior
	{
		[InspectorValue] protected Vector3 _target;

		public Enemy Enemy { get; set; }
		public bool HasTarget { get; protected set;}

		protected TargetingBehavior(Enemy enemy)
		{
			Enemy = enemy;
		}
		
		public abstract bool AcquireTarget();

		public Vector3 GetTarget()
		{
			return _target;
		}
	}
}
