using UnityEngine;
using Weapons;
using System.Collections;

public class TestEnemy : BaseEnemy
{
    protected override void PreStart()
    {
    }

    protected override void PostStart()
    {
    }
	
	protected override void Update()
	{
		if(Enemy != null)
		{
			Enemy.Update();
		}
	}
}
