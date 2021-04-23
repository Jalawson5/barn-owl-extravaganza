using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DebuffAction", menuName = "DebuffAction")]
public class DebuffAction : ActionSkill
{
    public int subATK;
    public int subMATK;
    public int subDEF;
    public int subMDEF;
    public int subAGI;
    public int subLUCK;
    
    public StatusEffect[] effects;
    
    public AttackEntry attack; //Debuff skills do not deal damage directly, set baseDamage to 0!//
}
