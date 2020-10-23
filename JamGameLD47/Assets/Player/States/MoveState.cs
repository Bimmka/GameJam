using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private float horizontal;


    public override void EnterState(PlayerController player)
    {
        player.SetAnimation(0);
    }

    public override void Update(PlayerController player)
    {
        if (player.IsDead) player.TransitionToState(player.deathState);


        if (Input.GetKey(KeyCode.A)) horizontal = -1;
        else if (Input.GetKey(KeyCode.D)) horizontal = 1;
        else horizontal = 0;

        player.SetMove( horizontal);
        player.SetAnimation(Mathf.Abs(horizontal));
        
    }
}
