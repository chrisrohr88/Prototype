using Weapons;
using Weapons.Behaviors;
using Weapons.Enums;

public class EnemyProfile : BaseProfile
{
	public int Level { get; set; }
	public int LayerMask { get; set; }
	public int Speed { get; set; }
	public float BaseHealth { get; set; }
	public int PointValue { get; set; }
	public string EnemyPrefabPath { get; set; }
	public string WeaponProfileName { get; set; }
	public MovementBehaviorType MovementBehaviorType { get; set; }
	public TargetingBehaviorType TargetingBehaviorType { get; set; }
	public AttackBehaviorType AttackBehaviorType { get; set; }
}
