using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public int id;
    public new string name;
    public WeaponType type;
    
    public int attack;
    public int mattack;
    public int bonusSTR;
    public int bonusINT;
    public int bonusEND;
    public int bonusSPR;
    public int bonusAGI;
    public int bonusLUCK;
    public Element element;
}
