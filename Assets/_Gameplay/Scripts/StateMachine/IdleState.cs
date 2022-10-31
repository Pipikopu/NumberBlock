using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public override void EnterState(BattleStateMachine stateMachine)
    {
        //Set Anim
        stateMachine.animator.SetTrigger("Idle");
    }

    public override void UpdateState(BattleStateMachine stateMachine)
    {
    }

    public override void ExitState(BattleStateMachine stateMachine)
    {
    }


}
