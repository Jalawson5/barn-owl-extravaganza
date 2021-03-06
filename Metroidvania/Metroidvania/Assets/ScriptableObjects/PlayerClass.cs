using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerClass", menuName = "PlayerClass")]
public class PlayerClass : ScriptableObject
{
    public int id;
    public new string name;
    
    public int baseHP;
    public int baseMP;
    
    public int growthHP;
    public int growthMP;
    public int growthSTR;
    public int growthINT;
    public int growthEND;
    public int growthSPR;
    public int growthAGI;
    public int growthLUCK;
}