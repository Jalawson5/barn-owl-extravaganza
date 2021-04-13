using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackEntry", menuName = "AttackEntry")]
public class AttackEntry : ScriptableObject
{
    public float baseAttack; //multiplier//
    public float duration;
    public bool isElemental;
    public bool isMagical; //Magical attacks based on INT//
    public bool hasProjectile;
    public bool hasStatus;
    public int status; //ID of status effect, if applicable//
    public float statusChance; //[0, 1)//
    public GameObject hitbox;
    public GameObject projectile; //null if !hasProjectile//
}
