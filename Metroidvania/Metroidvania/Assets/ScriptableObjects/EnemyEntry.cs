using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyEntry : ScriptableObject
{
    public int id;
    public new string name;
    public string description;

    public int enemyHP;
    public int enemyATK; //Enemies have a flat attack stat, rather than a strength stat//
    public int enemyMATK; //Enemies have a flat magic attack stat, rather than an intellect stat//
    public int enemyDEF; //Enemies have a flat defense stat, rather than an endurance stat//
    public int enemyMDEF; //Enemies have a flat magic defense stat, rather than a spirit stat//
    public int enemyAGI;
    public int enemyLUCK;
    
    public int expDrop;
    public int goldDrop;
}
