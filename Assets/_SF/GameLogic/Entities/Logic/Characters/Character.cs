using SF.GameLogic.Entities.Logic.Components;

namespace SF.GameLogic.Entities.Logic.Charaters
{
	public class Character : Entity
	{
		public HealthComponent Health { get; set; }

		public bool IsDead()
		{
			return Health.IsDead;
		}
	}
	
}
