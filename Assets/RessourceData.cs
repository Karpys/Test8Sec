using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RessourceData
{
    public int RessourceCount;
    public int HightScore;

    public RessourceData(RessourceManager Save)
    {
        RessourceCount = Save.RessourceCount;
        HightScore = Save.HightScore;
    }

    public RessourceData()
    {
        RessourceCount = 0;
        HightScore = 0;
    }
}
