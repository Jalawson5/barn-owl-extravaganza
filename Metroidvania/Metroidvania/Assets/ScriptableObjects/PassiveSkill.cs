using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PassiveSkill", menuName = "PassiveSkill")]
public class PassiveSkill : Skill
{
    public int bonusSTR;
    public int bonusINT;
    public int bonusEND;
    public int bonusSPR;
    public int bonusAGI;
    public int bonusLUCK;
}
