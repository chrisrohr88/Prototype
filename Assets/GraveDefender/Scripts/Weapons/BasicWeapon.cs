using UnityEngine;
using System.Collections;

public class BasicWeapon : Weapon
{
    public override void TriggerHeld()
    {
        Use();
    }

    public override void TriggerReleased()
    {
        Use();
    }

    public override void TriggerPulled()
    {
        Use();
    }
}
