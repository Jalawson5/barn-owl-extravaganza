using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuffAction", menuName = "BuffAction")]
public class BuffAction : ActionSkill
{
    public int bonusSTR;
    public int bonusEND;
    public int bonusINT;
    public int bonusSPR;
    public int bonusAGI;
    public int bonusLUCK;
    
    public BuffStatus[] buffs;
    
    public float duration;
    
    //If multiplayer is implemented, will include a new friendly AttackEntry type, hitting only allies with beneficial effects//
}
