using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "ScriptableObj/Level", order = 1)]
public class LevelSO : ScriptableObject
{
    public int playerStrength;
    public List<RowSO> rowSOs;
    public int numOfEnemies;
    public List<EnemySerial> enemiesSerial;
}
