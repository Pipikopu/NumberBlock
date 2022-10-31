using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(BattleStateMachine stateMachine);

    public abstract void UpdateState(BattleStateMachine stateMachine);

    public abstract void ExitState(BattleStateMachine stateMachine);
}
