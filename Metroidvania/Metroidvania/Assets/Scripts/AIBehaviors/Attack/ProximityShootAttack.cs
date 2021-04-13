using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityShootAttack : MonoBehaviour
{
    public GameObject targetPlayer;
    private Transform targetTransform;
    
    public EnemyBehavior parent;
    
    public AttackEntry attack;
    
    public bool isSmart; //Will this enemy aim towards the player?//
    public bool isRapid; //Will this enemy fire multiple projectiles?//
    
    public float projSpeed;
    
    public Transform[] targetAngles; //Nodes to launch towards, will create one projectile per node//
    public int rapidCount; //How many times to rapid fire?//
    public float rapidDelay; //How long to wait between shots in rapid?//
    
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
        for(int i = 0; i < targetAngles.Length; i++)
        {
            GameObject temp;
            temp = Instantiate(attack.projectile, transform, false);
            temp.GetComponent<EnemyProjectileBehavior>().Attack(parent.GetDirection());
            temp.GetComponent<EnemyProjectileBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
            temp.GetComponent<EnemyProjectileBehavior>().parentStats = parent.stats;
            temp.GetComponent<Rigidbody2D>().AddForce((targetAngles[i].position - temp.transform.position).normalized * projSpeed, ForceMode2D.Impulse);
        }
    }
}
