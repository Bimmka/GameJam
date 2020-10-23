using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    public override void EnterState(PlayerController player)
    {
        player.Death();
    }

    public override void Update(PlayerController player)
    {
        
    }
}
