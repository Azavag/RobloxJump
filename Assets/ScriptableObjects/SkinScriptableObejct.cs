using System;
using UnityEngine;

[Serializable]
public class SkinStats
{
    public uint passiveStats;
    public uint activeStats;
}


[CreateAssetMenu(fileName = "NewSkinScriptableObject", menuName = "Custom/Create Skin Scriptable Object")]
public class SkinScriptableObejct : ScriptableObject
{ 
    public SkinType skinType;
    public int idNumber;
    public string skinName;
    public int price;
    public bool isAdsReward;
    public Sprite sprite;
    public SkinStats skinStats;
    //private void OnValidate()
    //{
    //    if (skinType == SkinType.Hat)
    //        skinName = name;
    //}
}
