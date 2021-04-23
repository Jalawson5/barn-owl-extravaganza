using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new HealAction", menuName = "HealAction")]
public class HealAction : ActionSkill
{
    public float healHP;    
    public StatusEffect[] healEffects;
    
    //If multiplayer is implemented, will include a new friendly AttackEntry type, hitting only allies with beneficial effects//
}
