using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Asset Chance")]

public class Chance : ScriptableObject
{
    [SerializeField] int coinChance;

    public int CoinChance
    {
        get
        {
            return coinChance;
        }
    }
}
