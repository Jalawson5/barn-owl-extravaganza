using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuffItem")]
public class BuffItem : UseItem
{
    public int bonusSTR;
    public int bonusEND;
    public int bonusINT;
    public int bonusSPR;
    public int bonusAGI;
    public int bonusLUCK;
    public int regenHP;
    public int regenMP;
    
    public StatusEffect[] resist;
    public float resistAmount;
    
    public float duration;
}
