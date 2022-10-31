using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float timeCounter;

    public override void EnterState(BattleStateMachine stateMachine)
    {
        timeCounter = 0;

        // Set Anim
        stateMachine.animator.SetBool("IsIdle", true);
        stateMachine.animator.SetBool("IsAttack", true);
    }

    public override void UpdateState(BattleStateMachine stateMachine)
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= stateMachine.timeAttack)
        {
            stateMachine.animator.SetBool("IsAttack", false);
            BattleManager.Ins.EndTurn(stateMachine);
            stateMachine.SwitchState(stateMachine.idleState);
        }
    }

    public override void ExitState(BattleStateMachine stateMachine)
    {
    }
}
