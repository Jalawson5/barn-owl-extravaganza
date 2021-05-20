using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Race", menuName = "Character Race")]
public class PlayerRace : ScriptableObject
{
    public int id;
    public new string name;
    
    public int baseSTR;
    public int baseINT;
    public int baseEND;
    public int baseSPR;
    public int baseAGI;
    public int baseLUCK;
}
