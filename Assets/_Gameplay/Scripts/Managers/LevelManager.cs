using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelManager : Singleton<LevelManager>, IPointerDownHandler
{
    public Player player;
    public LevelSO levelSO;
    public Transform enemyStartingTransform;
    private Dictionary<int, List<Enemy>> rowIndexToEnemies = new Dictionary<int, List<Enemy>>();
    private int remainEnemies;

    public GameObject menuWindow;


    //TouchPhase touchPhase = TouchPhase.Ended;

    private void Start()
    {
        // Set Player
        SetPlayer();
        // Set Enemy
        SetEnemy();
    }

    private void Update()
    {
        
    }

    private void SetPlayer()
    {
        player.SetStrength(levelSO.playerStrength);
        remainEnemies = levelSO.numOfEnemies;
    }

    private void SetEnemy()
    {
        int numOfRows = levelSO.rowSOs.Count;
        for (int i = 0; i < numOfRows; i++)
        {
            RowSO currentRowSO = levelSO.rowSOs[i];
            for (int j = 0; j < currentRowSO.enemySOs.Count; j++)
            {
                EnemySO currentEnemySO = currentRowSO.enemySOs[j];
                float enemyX = 12f / numOfRows * i - 12f / numOfRows * (numOfRows - 1) / 2;
                float enemyZ = j * 4f;
                Vector3 newEnemyPos = enemyStartingTransform.position + Vector3.right * enemyX + Vector3.forward * enemyZ;
                newEnemyPos.y = 0;
                GameObject newEnemyGO = Instantiate(currentEnemySO.enemyPrefab, newEnemyPos, Quaternion.identity);
                Enemy newEnemy = newEnemyGO.GetComponent<Enemy>();
                newEnemy.SetType(currentEnemySO.enemyType);
                newEnemy.SetStrength(currentEnemySO.strength);
                newEnemy.SetRowIndex(i);
                newEnemy.SetEnemyState(Constant.CharacterState.IN_QUEUE);

                if (!rowIndexToEnemies.ContainsKey(i))
                {
                    rowIndexToEnemies[i] = new List<Enemy>();
                }
                rowIndexToEnemies[i].Add(newEnemy);
            }
        }
    }

    public void RearrangeRows(int rowIndex)
    {
        remainEnemies -= 1;
        for(int i = 0; i < rowIndexToEnemies[rowIndex].Count; i++)
        {
            Enemy currentEnemy = rowIndexToEnemies[rowIndex][i];
            if (currentEnemy == null)
            {
                continue;
            }
            else
            {
                currentEnemy.MoveToFront();
            }
        }
    }

    public int GetRemainNumOfEnemies()
    {
        return remainEnemies;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void PlayGame()
    {
        menuWindow.SetActive(false);
        player.AllowPlay();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
}
