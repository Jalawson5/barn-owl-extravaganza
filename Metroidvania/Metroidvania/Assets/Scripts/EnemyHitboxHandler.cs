using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnemyHitboxHandler sends messages to the enemy's behavior script//
public class EnemyHitboxHandler : MonoBehaviour
{
    private EnemyBehavior parentScript;
    
    void Start()
    {
        parentScript = transform.parent.gameObject.GetComponent<EnemyBehavior>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 10) //Player layer, inflict touch damage//
        {
            other.gameObject.GetComponent<PlayerController>().TakeTouchDamage(parentScript.stats);
        }
    }
}
