using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Implements base enemy behaviors//
public class EnemyBehavior : MonoBehaviour
{
    public EnemyEntry stats;
    public GameObject targetPlayer;
    private Transform targetTransform;
    private int currentHP;
    private float distance;
    private int direction;
    
    void Start()
    {
        currentHP = stats.enemyHP;
        targetTransform = targetPlayer.transform;
        distance = 0;
    }
    
    void Update()
    {
        distance = Vector3.Distance(transform.position, targetTransform.position);
    }
    
    /////////////////////////////////////////////////////////////////////////////////////
    //void TakeDamage(CharacterData.CharacterEntry, int float, bool                    //
    //Takes damage using the appropriate stats                                         //
    //CharacterData.CharacterEntry other - The CharacterEntry representing the attacker//
    //int otherWeapon - The attacker's weapon attack                                   //
    //float attackBase - The base attack multiplier of the attack                      //
    //bool isMagical - is the attack magical?                                          //
    /////////////////////////////////////////////////////////////////////////////////////
    public void TakeDamage(CharacterData.CharacterEntry other, float attackBase, bool isMagical)
    {
        int damage;
        
        if(isMagical)
        {
            damage = DamageCalculator.CalculateMagicalDamage(other.GetMATK(), attackBase, stats.enemyMDEF);
        }
         
        else
        {
            damage = DamageCalculator.CalculatePhysicalDamage(other.GetATK(), attackBase, stats.enemyDEF);
        }
        
        currentHP -= damage;
        Debug.Log("Damage Taken: " + damage);
        
        if(currentHP < 0)
            currentHP = 0;
            
        if(currentHP == 0)
        {
            Debug.Log("Enemy Defeated!");
        }
    }
    
    public float GetDistance()
    {
        return distance;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////////
    //void SetDirection()                                                                //
    //Used by Enemy Movement Scripts to tell the enemy what direction it should be facing//
    ///////////////////////////////////////////////////////////////////////////////////////
    public void SetDirection(int direction)
    {
        this.direction = direction;
    }
    
    ///////////////////////////////////////////////////////////////////////////////
    //int GetDirection()                                                         //
    //Used by Enemy Attack Scripts to determine the direction of attack hitboxes.//
    ///////////////////////////////////////////////////////////////////////////////
    public int GetDirection()
    {
        return this.direction;
    }
}
