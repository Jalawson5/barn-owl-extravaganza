using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UseItem", menuName = "UseItem")]
public class UseItem : ScriptableObject
{
    public int id;
    public new string name;
    
    public ItemEffect[] effects;
}
