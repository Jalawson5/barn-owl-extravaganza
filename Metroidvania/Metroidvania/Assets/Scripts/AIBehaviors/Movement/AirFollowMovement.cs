using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFollowMovement : MonoBehaviour
{
    public float moveSpeed;
    public float moveDelay; //wait between movements, if applicable. 0 for no delay//
    public float aggroRange; //Distance at which the enemy will start moving//
    public float offset; //What distance will this enemy keep?//
    
    public EnemyBehavior parent;
    
    public GameObject playerTarget; //Target to chase//
    private Transform playerTransform;
    
    private float moveTimer;
    private bool aggroed; //Is this enemy currently aggroed?//
    private bool chasing; //Is this enemy currently moving?//
    private Vector3 targetPos;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = 0f;
        aggroed = false;
        chasing = false;
        playerTransform = playerTarget.transform;
        direction = playerTarget.GetComponent<PlayerController>().GetDirection();
        parent.SetDirection(direction);
    }

    // Update is called once per frame
    void Update()
    {
        if(!aggroed) //If not aggroed, check aggro range for player//
        {
            if(parent.GetDistance() <= aggroRange)
            {
                aggroed = true;
            }
        }
        
        if(aggroed)
        {
            moveTimer += Time.deltaTime;
            if(moveTimer >= moveDelay)
            {
                moveTimer = 0;
                chasing = true;
                
                direction = playerTarget.GetComponent<PlayerController>().GetDirection();
                parent.SetDirection(direction);
                targetPos = new Vector3(playerTransform.position.x + offset * direction, playerTransform.position.y + offset, 0);
            }
        }
        
        if(chasing) //Even if aggro is broken, finish moving to target//
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            
            if(Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                chasing = false;
            }
        }
    }
}
