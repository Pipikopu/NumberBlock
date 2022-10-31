using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    //private float timeCounter;

    public override void EnterState(BattleStateMachine stateMachine)
    {
        stateMachine.animator.SetTrigger("Attack");
    }

    public override void UpdateState(BattleStateMachine stateMachine)
    {
    }

    public override void ExitState(BattleStateMachine stateMachine)
    {

    }
}
