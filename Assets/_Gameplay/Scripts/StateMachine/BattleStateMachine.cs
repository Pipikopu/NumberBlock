using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    [Header("State Machine")]
    public Transform tf;
    public BattleStateMachine stateMachine;

    [Header("States")]
    private BaseState currentState;
    public StartState startState = new StartState();
    public IdleState idleState = new IdleState();
    public AttackState attackState = new AttackState();
    public WinState winState = new WinState();
    public DeadState deadState = new DeadState();
    public ComboState comboState = new ComboState();

    [Header("Main Object Variables")]
    public float speed;
    public int strength;
    public float health;
    public Transform battleTransform;
    public Transform initTransform;
    public Transform getHitTransform;

    [Header("Animator")]
    public Animator animator;

    [Header("Time Variables")]
    public float timeAttack = 0f;

    private void OnEnable()
    {
        OnInit();
    }

    public void OnInit()
    {
        currentState = startState;
        currentState.EnterState(stateMachine);
    }

    private void Update()
    {
        currentState.UpdateState(stateMachine);
    }

    public void SwitchState(BaseState newState)
    {
        if (currentState != newState && currentState != deadState)
        {
            currentState.ExitState(stateMachine);
            currentState = newState;
            currentState.EnterState(stateMachine);
        }
    }

    public void GetHit()
    {
        //StartCoroutine(IGetHit());
    }

    IEnumerator IGetHit()
    {
        while (Vector3.Distance(tf.position, getHitTransform.position) < 0.1f)
        {
            tf.position = Vector3.MoveTowards(tf.position, getHitTransform.position, Time.deltaTime * 0.05f);
        }
        tf.position = getHitTransform.position;
        

        yield return new WaitForSeconds(0.05f);

        while (Vector3.Distance(tf.position, battleTransform.position) < 0.1f)
        {
            tf.position = Vector3.MoveTowards(tf.position, battleTransform.position, Time.deltaTime * 0.05f);
        }
        tf.position = battleTransform.position;
    }
}
