using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    public Constant.EnemyType enemyType;
    private Constant.CharacterState enemyState;
    public int strength;
    public BoxCollider enemyCollider;

    [Header("EnemyUI")]
    public Text strengthText;
    public Text healthText;

    [Header("Position Variables")]
    private int rowIndex;
    public bool canBeChosen = true;

    private void Start()
    {
        canBeChosen = true;
    }

    // Click on the Enemy
    public void OnBeingChosen()
    {
        if (canBeChosen)
        {
            canBeChosen = false;

            // Arrange other enemies
            LevelManager.Ins.RearrangeRows(rowIndex);

            LevelManager.Ins.PreventChooseEnemy();

            // Battle
            BattleManager.Ins.MoveToBattle(strength);

            // Remove from choosing field
            Destroy(this.gameObject);
        }
    }

    public void AllowBeingChosen()
    {
        enemyCollider.enabled = true;
    }

    private void Update()
    {
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                if (Physics.Raycast(ray, out hit))
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                }
            }

        }
    }

    public void SetType(Constant.EnemyType newType)
    {
        enemyType = newType;
    }

    public void SetStrength(int newStrength)
    {
        strength = newStrength;
        strengthText.text = strength.ToString();
        healthText.text = strength.ToString();
    }

    public void SetRowIndex(int newRowIndex)
    {
        rowIndex = newRowIndex;
    }

    public int GetStrength()
    {
        return strength;
    }

    public void MoveToFront()
    {
        transform.position -= Vector3.forward * 4f;
    }

    public void SetEnemyState(Constant.CharacterState newState)
    {
        enemyState = newState;
        switch (enemyState)
        {
            case Constant.CharacterState.IN_QUEUE:
                healthText.gameObject.SetActive(false);
                strengthText.gameObject.SetActive(true);
                break;
            case Constant.CharacterState.IN_BATTLE:
                healthText.gameObject.SetActive(true);
                strengthText.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
