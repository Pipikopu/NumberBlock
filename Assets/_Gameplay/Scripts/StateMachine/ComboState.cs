using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboState : BaseState
{
    //private float timeCounter;

    public override void EnterState(BattleStateMachine stateMachine)
    {
        //timeCounter = 0;
        stateMachine.animator.SetTrigger("Combo");
    }

    public override void ExitState(BattleStateMachine stateMachine)
    {
    }

    public override void UpdateState(BattleStateMachine stateMachine)
    {
    }
}
