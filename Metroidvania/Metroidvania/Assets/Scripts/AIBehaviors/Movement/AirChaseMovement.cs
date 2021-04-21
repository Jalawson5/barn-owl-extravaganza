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
    private float height;
    private float width;
    
    private LayerMask terrainLayer;

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
        
        terrainLayer = LayerMask.GetMask("Solid");
        
        height = gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        width = gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2;
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
                
                //Check if the enemy will move into a nearby wall, adjust the target position if true//
                RaycastHit2D hit = Physics2D.Raycast(transform.position, DirectionVector(), 2f, terrainLayer);
                RaycastHit2D hitBottom = Physics2D.Raycast(new Vector3(transform.position.x + (width * parent.GetDirection()), transform.position.y - height, 0), DirectionVector(), 2f, terrainLayer);
                RaycastHit2D hitTop = Physics2D.Raycast(new Vector3(transform.position.x + (width * parent.GetDirection()), transform.position.y + height, 0), DirectionVector(), 2f, terrainLayer);
                
                if((hit.collider != null || hitBottom.collider != null || hitTop.collider != null) && !throughWalls)
                {
                    AdjustTarget();
                }
            }
        }
        
        if(chasing) //Even if aggro is broken, finish moving to target//
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            Vector3 direction = Vector3.Normalize(targetPos - transform.position);
            rb.velocity = direction * moveSpeed;
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2f, terrainLayer);
            if(hit.collider != null)
            {
                //AdjustVelocity();
            }
            
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
    
    private void AdjustVelocity()
    {
        float tempX, tempY;
        int xDir, yDir;
        
        xDir = ((rb.velocity.x < 0)? -1: 1);
        yDir = ((rb.velocity.y < 0)? -1: 1);
        
        tempX = rb.velocity.y * xDir;
        tempY = rb.velocity.x * yDir;
        
        rb.velocity = new Vector3(tempX, tempY, 0);
    }
    
    private void AdjustTarget()
    {
        float tempX, tempY;
        int xDir, yDir;
        
        Vector3 tempVect = transform.InverseTransformPoint(targetPos);
        
        xDir = ((tempVect.x < 0)? -1: 1);
        yDir = ((tempVect.y < 0)? -1: 1);
        
        tempX = tempVect.y;
        tempY = tempVect.x;
        
        //If X is the wrong direction, flip the sign//
        if((xDir == -1 && tempX > 0) || (xDir == 1 && tempX < 0))
            tempX *= -1;
        
        //If Y is the wrong direction, flip the sign//
        if((yDir == -1 && tempY > 0) || (yDir == 1 && tempY < 0))
            tempY *= -1;
            
        targetPos = new Vector3(transform.position.x + tempX, transform.position.y + tempY, 0);
    }
    
    private Vector3 DirectionVector()
    {
        return Vector3.Normalize(targetPos - transform.position);
    }
}