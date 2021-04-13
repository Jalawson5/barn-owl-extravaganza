using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirChaseMovement : MonoBehaviour
{
    public float moveSpeed;
    public float moveDelay; //wait between movements, if applicable. 0 for no delay//
    public float aggroRange; //Distance at which the enemy will start moving//
    public bool throughWalls; //Can this enemy move through walls?//
    public bool aggressive; //Will this enemy continue to chase a far away player?//
    public bool perfectFollow; //Will this enemy perfectly follow the player?
    
    public EnemyBehavior parent;
    
    public GameObject playerTarget; //Target to chase//
    private Transform playerTransform;
    private Rigidbody2D rb;
    
    private float moveTimer;
    private bool aggroed; //Is this enemy currently aggroed?//
    private bool chasing; //Is this enemy currently moving?//
    private Vector3 targetPos;
    private float timeout; //If timeout time is met, may be stuck, stop chasing.//
    private float timeoutTimer;

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = 0f;
        aggroed = false;
        chasing = false;
        playerTransform = playerTarget.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        if(throughWalls)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        
        timeout = 2f;
        timeoutTimer = 0f;
        
        parent.SetDirection(-1);
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
            
            /*else
            {
                rb.velocity = new Vector3(0, 0, 0);
                chasing = false;
            }*/
        }
        
        else if(!aggressive) //If not aggressive, check if player has left aggro range//
        {
            if(parent.GetDistance() > aggroRange)
            {
                aggroed = false;
                moveTimer = 0;
            }
        }
        
        if(aggroed && (!chasing || perfectFollow))
        {
            moveTimer += Time.deltaTime;
            if(moveTimer >= moveDelay)
            {
                moveTimer = 0;
                timeoutTimer = 0;
                chasing = true;
                
                targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
            }
        }
        
        if(chasing) //Even if aggro is broken, finish moving to target//
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            Vector3 direction = Vector3.Normalize(targetPos - transform.position);
            rb.velocity = direction * moveSpeed;
            
            timeoutTimer += Time.deltaTime;
            
            if(Vector3.Distance(transform.position, targetPos) < 0.1f || timeoutTimer >= timeout)
            {
                chasing = false;
            }
        }
        
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        
        if(rb.velocity.x <= 0)
        {
            parent.SetDirection(-1);
        }
        
        else
        {
            parent.SetDirection(1);
        }
    }
}
