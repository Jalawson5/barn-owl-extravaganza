using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueEntry : ScriptableObject
{
    public int id;
    public string[] texts;
}
