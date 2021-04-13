using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySlashAttack : MonoBehaviour
{
    public GameObject targetPlayer;
    private Transform targetTransform;
    
    public EnemyBehavior parent;
    
    public AttackEntry attack;
    
    public float aggroRange; //Range at which this enemy will start attacking//
    public float attackDelay; //How long this enemy will wait before first attacking//
    public float attackTimer; //How long this enemy will wait between attacks//
    
    private bool aggroed;
    private bool firstAttack;
    private float firstTimer;
    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        aggroed = false;
        firstAttack = false;
        firstTimer = 0;
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!aggroed)
        {
            if(parent.GetDistance() <= aggroRange)
            {
                aggroed = true;
            }
        }
        
        else
        {
            if(parent.GetDistance() > aggroRange)
            {
                aggroed = false;
            }
        }
        
        if(aggroed)
        {
            if(!firstAttack)
            {
                firstTimer += Time.deltaTime;
                
                if(firstTimer >= attackDelay)
                {
                    Attack();
                
                    firstAttack = true;
                    firstTimer = 0;
                }
            }
            
            else
            {
                cooldown += Time.deltaTime;
                
                if(cooldown >= attackTimer)
                {
                    Attack();
                    
                    cooldown = 0;
                }
            }
        }
    }
    
    private void Attack()
    {
        GameObject temp;
        temp = Instantiate(attack.hitbox, transform, false);
        temp.GetComponent<EnemyAttackHitboxBehavior>().Attack(attack.duration, parent.GetDirection());
        temp.GetComponent<EnemyAttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
    }
}
