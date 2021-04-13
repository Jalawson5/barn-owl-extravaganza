using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradeSkill", menuName = "UpgradeSkill")]
public class UpgradeSkill : Skill
{
    public ActionSkill baseSkill;
    
    public float damageMultiplier;
    public float costMultiplier;
}
