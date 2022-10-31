using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRow", menuName = "ScriptableObj/Row", order = 1)]
public class RowSO : ScriptableObject
{
    public List<EnemySO> enemySOs;
}
