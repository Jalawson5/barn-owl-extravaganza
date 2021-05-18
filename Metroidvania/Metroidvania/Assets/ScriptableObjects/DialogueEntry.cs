using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueEntry : ScriptableObject
{
    public int id;
    public string text;
    
    public bool hasChoice;
    public string choice1;
    public string choice2;
    public DialogueEntry result1;
    public DialogueEntry result2;
}
