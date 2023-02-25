using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TurnManager
{
    public const int MAX_TURNS_CALCULATED_IN_ADVANCE = 5;


    public static List<int> StartBattleTurns(List<int> battleParticipantsSpeed, List<int> initiative)
    {
        return CalculateTurnOrder(battleParticipantsSpeed, initiative);
    }

    public static List<int> CalculateMoreTurns(List<int> battleParticipants, List<int> initiative)
    {
        return CalculateTurnOrder(battleParticipants, initiative);
    }

    private static List<int> CalculateTurnOrder(List<int> battleParticipantsSpeed, List<int> initiative)
    {
        List<int> turnOrder = new List<int>();
        for (int i = 0; i < MAX_TURNS_CALCULATED_IN_ADVANCE; i++)
        {
            for (int j = 0; j < battleParticipantsSpeed.Count; j++)
            {
                initiative[j] += battleParticipantsSpeed[j];
                if (initiative[j] >= 100)
                {
                    turnOrder.Add(j);
                    initiative[j] -= 100;
                }
            }
        }
        return turnOrder;
    }
}
