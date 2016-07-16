using SF.GameLogic.Data.Enums;

namespace SF.GameLogic.Data.Profiles
{
	public class EnemyProfile : BaseProfile
	{
		public int Level;
		public int LayerMask;
		public int Speed;
		public float BaseHealth;
		public int PointValue;
		public string EnemyPrefabPath;
		public string WeaponProfileName;
		public MovementBehaviorType MovementBehaviorType;
		public TargetingBehaviorType TargetingBehaviorType;
		public AttackBehaviorType AttackBehaviorType;
	}
}
