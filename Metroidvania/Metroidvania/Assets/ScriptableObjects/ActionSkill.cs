using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActionSkill", menuName = "ActionSkill")]
public class ActionSkill : Skill
{
    public int cost;
    public SkillData.TargetType target;
}
