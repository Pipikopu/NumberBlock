using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    public override void EnterState(BattleStateMachine stateMachine)
    {
        // Set Anim
        stateMachine.animator.SetBool("IsAttack", false);
        stateMachine.animator.SetBool("IsDead", true);
    }

    public override void UpdateState(BattleStateMachine stateMachine)
    {
    }

    public override void ExitState(BattleStateMachine stateMachine)
    {
    }


}
