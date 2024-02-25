using System;
using UnityEngine;

public class SkinCharacteristics : MonoBehaviour
{
    private uint passiveStats;
    private uint activeStats;

    public static event Action CharacteristicsChanged;

    public void SetStatsFromSkin(SkinCard skinCard)
    {
        passiveStats = skinCard.skinScriptable.skinStats.passiveStats;
        activeStats = skinCard.skinScriptable.skinStats.activeStats;
        CharacteristicsChanged?.Invoke();
    }

    public uint GetPassiveStats()
    {
        return passiveStats;
    }
    public uint GetActiveStats()
    {
        return activeStats;
    }
}
