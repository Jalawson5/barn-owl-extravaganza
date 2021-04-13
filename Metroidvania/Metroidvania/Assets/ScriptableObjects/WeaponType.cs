using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponType", menuName = "WeaponType")]
public class WeaponType : ScriptableObject
{
    public AttackEntry neutral;
    public AttackEntry forward;
    public AttackEntry downward;
    public AttackEntry upward;
    public AttackEntry neutralA;
    public AttackEntry downwardA;
    public AttackEntry upwardA;
    
    public float speed;
    
    public bool canCharge;
    public AttackEntry shortCharge;
    public AttackEntry longCharge;
}
