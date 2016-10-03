using SF.GameLogic.Entities.Logic.Components;
using SF.CustomInspector.Attributes;

namespace SF.GameLogic.Entities.Logic.Charaters
{
	public class Character : Entity
	{
		[InspectorObject(Label = "Health")] public HealthComponent Health { get; set; }

		public bool IsDead()
		{
			return Health.IsDead;
		}
	}
	
}
