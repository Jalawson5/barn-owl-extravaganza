using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxBehavior : MonoBehaviour
{
    private float timer;
    private bool active;
    private bool direction;
    
    private int weaponAttack;
    private float baseAttack;
    private bool isElemental;
    private bool isMagical;
    
    private PlayerController parentScript;
    
    void Start()
    {
        parentScript = transform.parent.gameObject.GetComponent<PlayerController>();
    }
    
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
                Destroy(gameObject);
        }
    }
    
    /////////////////////////////////////////////////////////////////////
    //void AttackTimer()                                               //
    //Starts the attack timer, after which the hitbox will be destroyed//
    /////////////////////////////////////////////////////////////////////
    public void Attack(float time, int direction)
    {
        timer = time;
        transform.localPosition = new Vector3(transform.localPosition.x * direction, transform.localPosition.y, 0);
    }
    
    public void InitStats(float baseAttack, bool isElemental, bool isMagical)
    {
        this.baseAttack = baseAttack;
        this.isElemental = isElemental;
        this.isMagical = isMagical;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 11) //Enemy layer//
        {
            other.gameObject.GetComponent<EnemyBehavior>().TakeDamage(parentScript.stats, baseAttack, isMagical);
        }
        
        else if(other.gameObject.tag == "Breakable")
        {
            //Do whatever animations//
            Destroy(other.gameObject);
        }
    }
}
