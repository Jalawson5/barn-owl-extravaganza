using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create Accessory", menuName = "Accessory")]
public class Accessory : ScriptableObject
{
    public int id;
    public new string name;
    
    public int bonusDEF;
    public int bonusMDEF;
    public int bonusSTR;
    public int bonusINT;
    public int bonusEND;
    public int bonusSPR;
    public int bonusAGI;
    public int bonusLUCK;
}
