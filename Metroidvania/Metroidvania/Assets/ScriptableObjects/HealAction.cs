using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new HealAction", menuName = "HealAction")]
public class HealAction : ActionSkill
{
    public float healHP;    
    public StatusEffect[] healEffects;
}
