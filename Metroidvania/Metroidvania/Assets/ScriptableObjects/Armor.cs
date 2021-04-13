using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Armor")]
public class Armor : ScriptableObject
{
    public int id;
    public new string name;
    public ArmorType type;
    
    public int defense;
    public int mdefense;
    public int bonusSTR;
    public int bonusINT;
    public int bonusEND;
    public int bonusSPR;
    public int bonusAGI;
    public int bonusLUCK;
}
