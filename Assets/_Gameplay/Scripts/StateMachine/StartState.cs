using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : BaseState
{
    private Transform machineTransform;

    private float speed;
    private Transform battleTransform;
    public override void EnterState(BattleStateMachine stateMachine)
    {
        stateMachine.animator.SetBool("IsIdle", false);
        machineTransform = stateMachine.transform;
        machineTransform.position = stateMachine.initTransform.position;
        speed = stateMachine.speed;
        battleTransform = stateMachine.battleTransform;
    }


    public override void UpdateState(BattleStateMachine stateMachine)
    {
        if (Vector3.Distance(machineTransform.position, battleTransform.position) >= 0.1f)
        {
            machineTransform.position = Vector3.MoveTowards(machineTransform.position, battleTransform.position, speed * Time.deltaTime);
        }
        else
        {
            // Check if start battle first
            if (BattleManager.Ins.GetCurrentStateMachine() == stateMachine)
            {
                stateMachine.SwitchState(stateMachine.idleState);
                //return;
            }
            else
            {
                BattleManager.Ins.EndTurn(stateMachine);
                stateMachine.SwitchState(stateMachine.idleState);
            }
        }
    }

    public override void ExitState(BattleStateMachine stateMachine)
    {
    }

}
