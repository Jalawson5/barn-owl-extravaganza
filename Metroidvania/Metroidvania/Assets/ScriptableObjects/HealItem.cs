using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HealItem", menuName = "HealItem")]
public class HealItem : UseItem
{
    public int healHP;
    public int healMP;
    public StatusEffect[] effects;
}
