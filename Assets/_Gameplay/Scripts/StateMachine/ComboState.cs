using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboState : BaseState
{
    private float timeCounter;

    public override void EnterState(BattleStateMachine stateMachine)
    {
        timeCounter = 0;

        stateMachine.animator.SetBool("IsCombo", true);
    }

    public override void ExitState(BattleStateMachine stateMachine)
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= stateMachine.timeAttack)
        {
            stateMachine.animator.SetBool("IsCombo", false);
            BattleManager.Ins.EndTurn(stateMachine);
            stateMachine.SwitchState(stateMachine.idleState);
        }

    }

    public override void UpdateState(BattleStateMachine stateMachine)
    {
    }
}
