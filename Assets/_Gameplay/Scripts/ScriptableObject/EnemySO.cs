using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObj/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    public Constant.EnemyType enemyType;
    public GameObject enemyPrefab;
    public int strength;
}
