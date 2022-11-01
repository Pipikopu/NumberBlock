using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BattleManager : Singleton<BattleManager>
{
    [Header("Battle Player")]
    public Player player;
    public BattleStateMachine battlePlayer;
    public GameObject battlePlayerGo;
    public Transform battlePlayerTransform;

    [Header("Battle Enemy")]
    public BattleStateMachine battleEnemy;
    public GameObject battleEnemyGO;
    public Transform battleEnemyTransform;

    [Header("UI")]
    public GameObject battleUI;
    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;

    [Header("StateMachine")]
    private BattleStateMachine currentStateMachine;

    private bool canBattle;

    private void Start()
    {
        canBattle = false;
    }

    public void MoveToBattle(int enemyStrength)
    {
        // Set Init UI
        battleUI.SetActive(true);
        enemyHealthSlider.value = 1;
        playerHealthSlider.value = 1;


        // Set Battle Player
        battlePlayer.strength = player.GetStrength();
        battlePlayer.health = player.GetStrength();
        battlePlayerGo.SetActive(true);


        // Set Battle Enemy
        SetEnemyData(enemyStrength);
        battleEnemyGO.SetActive(true);

        // Set Current Machine
        StartCoroutine(WaitForMachineToMove());
    }

    IEnumerator WaitForMachineToMove()
    {
        yield return new WaitForSeconds(1f);
        canBattle = true;
        currentStateMachine = battlePlayer;
    }

    public void Update()
    {
        if (canBattle)
        {
            canBattle = false;
            if (currentStateMachine == battlePlayer)
            {
                StartCoroutine(PlayerDealAttack());
            }
            else
            {
                StartCoroutine(EnemyDealAttack());
            }
        }
    }

    IEnumerator PlayerDealAttack()
    {
        yield return new WaitForSeconds(0.4f);
        // Start Attack
        if (battlePlayer.strength / 3 >= battleEnemy.health)
        {
            battlePlayer.SwitchState(battlePlayer.comboState);
            yield return new WaitForSeconds(0.75f);
            battleEnemy.SwitchState(battleEnemy.deadState);

            enemyHealthSlider.value = 0;

            player.IncreaseStrength(battleEnemy.strength);
            StartCoroutine(DoneBattle());
        }
        else
        {
            battlePlayer.SwitchState(battlePlayer.attackState);
            // Delay when enemy get hit
            yield return new WaitForSeconds(0.2f);
            battleEnemy.GetHit();

            battleEnemy.health -= (float)(battlePlayer.strength / 3);
            float newValue = battleEnemy.health / battleEnemy.strength;
            if (newValue < 0.15f && newValue > 0)
            {
                newValue = 0.15f;
            }
            enemyHealthSlider.value = newValue;

            canBattle = true;
            currentStateMachine = battleEnemy;
            battlePlayer.SwitchState(battlePlayer.idleState);
        }
    }

    IEnumerator EnemyDealAttack()
    {
        yield return new WaitForSeconds(0.4f);

        // Start Attack
        if (battleEnemy.strength / 3 >= battlePlayer.health)
        {
            battleEnemy.SwitchState(battleEnemy.comboState);
            yield return new WaitForSeconds(0.75f);
            battlePlayer.SwitchState(battlePlayer.deadState);

            playerHealthSlider.value = 0;
            player.Lose();
        }
        else
        {
            battleEnemy.SwitchState(battleEnemy.attackState);
            yield return new WaitForSeconds(0.2f);
            battlePlayer.GetHit();

            battlePlayer.health -= (float)(battleEnemy.strength / 3);
            float newValue = battlePlayer.health / battlePlayer.strength;
            if (newValue <= 0.15f && newValue > 0)
            {
                newValue = 0.15f;
            }
            playerHealthSlider.value = newValue;

            canBattle = true;
            currentStateMachine = battlePlayer;
            battleEnemy.SwitchState(battleEnemy.idleState);
        }
    }

    IEnumerator DoneBattle()
    {
        yield return new WaitForSeconds(1.5f);

        battleUI.SetActive(false);
        battlePlayerGo.SetActive(false);
        battleEnemyGO.SetActive(false);
        LevelManager.Ins.AllowChooseEnemy();
        if (LevelManager.Ins.GetRemainNumOfEnemies() == 0)
        {
            player.RunToWin();
        }
    }

    public BattleStateMachine GetCurrentStateMachine()
    {
        return currentStateMachine;
    }

    public BattleStateMachine GetBattlePlayer()
    {
        return battlePlayer;
    }

    private void SetEnemyData(int newStrength)
    {
        battleEnemy.strength = newStrength;
        battleEnemy.health = newStrength;
    }
}
