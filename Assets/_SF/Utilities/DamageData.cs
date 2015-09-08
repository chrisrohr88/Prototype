using UnityEngine;
using System.Collections;

public enum DamageType
{
    None,
    Holy,
    Fire,
    Shadow,
    Physical
}

public class DamageData : MonoBehaviour
{
    public long AttackerId { get; set; }
    public int TargetrId { get; set; }
    public float Damage { get; set; }
    public DamageType DamageType { get; set; }
}
