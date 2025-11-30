using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public static class SaveGame
{
    public static int Coins
    {
        get => PlayerPrefs.GetInt("coins",0);
        set => PlayerPrefs.SetInt("coins",value);
    }
}
