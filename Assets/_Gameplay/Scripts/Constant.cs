using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public const string ANIM_IS_IDLE = "IsIdle";

    public enum EnemyType
    {
        ENEMY_BLUE,
        ENEMY_RED,
        ENEMY_GREEN,
    }

    public enum CharacterState
    {
        IN_QUEUE,
        IN_BATTLE
    }

    public const string ENEMY_DATA_PATH = "/_Gameplay/JSONFiles/enemy.json";

    public const string MAIN_GAME_SCENE = "MainGameScene";
    public const string BATTLE_SCENE = "BattleField";
}