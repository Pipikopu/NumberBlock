using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    public List<EnemySO> enemySOs;
    private Dictionary<Constant.EnemyType, EnemySO> dictEnemySOs = new Dictionary<Constant.EnemyType, EnemySO>();

    private void Start()
    {
        for (int i = 0; i < enemySOs.Count; i++)
        {
            dictEnemySOs[enemySOs[i].enemyType] = enemySOs[i];
        }
    }

    //public void SaveBattleEnemy(Constant.EnemyType enemyType, int strength)
    //{
    //    if (File.Exists(Application.dataPath + Constant.ENEMY_DATA_PATH))
    //    {
    //        string enemyJson = File.ReadAllText(Application.persistentDataPath + Constant.ENEMY_DATA_PATH);
    //        EnemyData enemyData = JsonUtility.FromJson<EnemyData>(enemyJson);
    //        enemyData.enemyType = (int)enemyType;
    //        enemyData.strength = strength;
    //        enemyJson = JsonUtility.ToJson(enemyData);
    //        File.WriteAllText(Application.dataPath + Constant.ENEMY_DATA_PATH, enemyJson);
    //    }
    //    else
    //    {
    //        EnemyData enemyData = new EnemyData();
    //        enemyData.enemyType = 0;
    //        enemyData.strength = 0;
    //        string enemyJson = JsonUtility.ToJson(enemyData);
    //        File.WriteAllText(Application.dataPath + Constant.ENEMY_DATA_PATH, enemyJson);
    //    }
    //}

    //public EnemySO GetBattleEnemy()
    //{
    //    string enemyJson = File.ReadAllText(Application.persistentDataPath + Constant.ENEMY_DATA_PATH);
    //    EnemyData enemyData = JsonUtility.FromJson<EnemyData>(enemyJson);
    //    EnemySO enemySO = dictEnemySOs[(Constant.EnemyType)enemyData.enemyType];
    //    return enemySO;
    //}

    //public int GetBattleEnemyStrength()
    //{
    //    string enemyJson = File.ReadAllText(Application.persistentDataPath + Constant.ENEMY_DATA_PATH);
    //    EnemyData enemyData = JsonUtility.FromJson<EnemyData>(enemyJson);
    //    return enemyData.strength;
    //}
}
