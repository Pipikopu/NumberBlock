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
    //public Transform enemyHolderTransform;
    private Enemy enemy;

    [Header("UI")]
    public GameObject battleUI;
    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;

    [Header("StateMachine")]
    private BattleStateMachine currentStateMachine;

    public void MoveToBattle()
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
        //EnemySO battleEnemySO = DataManager.Ins.GetBattleEnemy();
        battleEnemy.strength = DataManager.Ins.GetBattleEnemyStrength();
        battleEnemy.health = DataManager.Ins.GetBattleEnemyStrength();
        battleEnemyGO.SetActive(true);


        // Set Current Machine
        currentStateMachine = battlePlayer;
    }

    public void EndTurn(BattleStateMachine stateMachine)
    {
        if (stateMachine == battleEnemy && battlePlayer.health >= 0)
        {
            StartCoroutine(PlayerDealAttack());
        }
        else if (stateMachine == battlePlayer && battleEnemy.health >= 0)
        {
            StartCoroutine(EnemyDealAttack());
        }
    }

    IEnumerator PlayerDealAttack()
    {
        // Start Attack
        if (battlePlayer.strength / 4 >= battleEnemy.health)
        {
            battlePlayer.SwitchState(battlePlayer.comboState);
        }
        else
        {
            battlePlayer.SwitchState(battlePlayer.attackState);
            yield return new WaitForSeconds(0.5f);
            battleEnemy.GetHit();
        }

        // Delay
        yield return new WaitForSeconds(0.5f);

        if (battlePlayer.health > 0)
        {
            // Set Health Bar
            battleEnemy.health -= (float)(battlePlayer.strength / 4);
            float newValue = battleEnemy.health / battleEnemy.strength;
            if (newValue < 0.15f && newValue > 0)
            {
                newValue = 0.15f;
            }
            enemyHealthSlider.value = newValue;

            // Check State
            if (battleEnemy.health <= 0)
            {
                battleEnemy.SwitchState(battleEnemy.deadState);
                player.IncreaseStrength(battleEnemy.strength);

                // Win All

                StartCoroutine(DoneBattle());
            }
        }
    }

    IEnumerator EnemyDealAttack()
    {
        // Start Attack
        if (battleEnemy.strength / 4 >= battlePlayer.health)
        {
            battleEnemy.SwitchState(battleEnemy.comboState);
        }
        else
        {
            battleEnemy.SwitchState(battleEnemy.attackState);
            yield return new WaitForSeconds(0.5f);
            battlePlayer.GetHit();
        }

        // Delay
        yield return new WaitForSeconds(0.5f);

        if (battleEnemy.health > 0)
        {
            // Set Health Bar
            battlePlayer.health -= (float)(battleEnemy.strength / 4);
            float newValue = battlePlayer.health / battlePlayer.strength;
            if (newValue <= 0.15f && newValue > 0)
            {
                newValue = 0.15f;
            }
            playerHealthSlider.value = newValue;

            // Check State
            if (battlePlayer.health <= 0)
            {
                battlePlayer.SwitchState(battlePlayer.deadState);
                player.Lose();
            }
        }
    }

    IEnumerator DoneBattle()
    {
        yield return new WaitForSeconds(1.5f);

        battleUI.SetActive(false);
        battlePlayerGo.SetActive(false);
        battleEnemyGO.SetActive(false);

        if (LevelManager.Ins.GetRemainNumOfEnemies() == 0)
        {
            player.RunToWin();
        }
    }

    public BattleStateMachine GetCurrentStateMachine()
    {
        return currentStateMachine;
    }
}
