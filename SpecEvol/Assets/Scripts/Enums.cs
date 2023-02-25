using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum BattleState
    {
        START,
        PLAYER_TURN,
        ENEMY_TURN,
        END
    }
    public enum AffectedStat
    {
        HEALTH,
        ATTACK,
        SPEED
    } 

    public enum DamageType
    {
        NONE,
        BONK,
        SLASHING,
        CHEMICAL,
        SHOCK
    }
}
