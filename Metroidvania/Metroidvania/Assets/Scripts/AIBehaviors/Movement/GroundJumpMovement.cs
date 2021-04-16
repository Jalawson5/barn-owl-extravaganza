using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundJumpMovement : MonoBehaviour
{
    public GameObject targetPlayer; //Target//
    private Transform targetTransform;
    
    public float moveDelay; //How long will the enemy wait to jump?//
    public float jumpForce; //How high should the enemy jump?//
    public float horizontalForce; //How far should the enemy jump?//
    public float aggroRange; //Distance at which the enemy will start moving//
    public bool aggressive; //Will this enemy continue chasing a far away player?//
    
    public EnemyBehavior parent;
    
    private float moveTimer;
    private bool aggroed;
    private bool grounded;
    private bool jumped;
    private int direction;
    private float height;
    private float width;
    
    private LayerMask terrainLayer;
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = targetPlayer.transform;
    
        moveTimer = 0f;
        aggroed = false;
        grounded = false;
        jumped = false;
        direction = -1;
        parent.SetDirection(-1);
        
        height = gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        width = gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        
        terrainLayer = LayerMask.GetMask("Solid");
        
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetTransform.position.x < transform.position.x)
        {
            direction = -1;
            parent.SetDirection(-1);
        }
        
        else
        {
            direction = 1;
            parent.SetDirection(1);
        }
    
        if(!aggroed) //If not aggroed, check aggro range for player//
        {
            if(parent.GetDistance() <= aggroRange)
            {
                aggroed = true;
            }
        }
        
        else if(!aggressive)
        {
            if(parent.GetDistance() > aggroRange)
            {
                aggroed = false;
            }
        }
        
        if(!grounded)
        {        
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(width, height, 0), Vector2.down, 0.1f, terrainLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(width, height * (-1), 0), Vector2.down, 0.1f, terrainLayer);
            
            if(hitLeft.collider != null || hitRight.collider != null)
            {
                grounded = true;
                
                if(jumped)
                {
                    moveTimer = 0;
                    jumped = false;
                }
            }
        }
        
        else
        {
            //rb.velocity = new Vector2(0, 0);
        }
        
        if(aggroed && grounded)
        {
            moveTimer += Time.deltaTime;
            
            if(moveTimer >= moveDelay)
            {
                grounded = false;
                jumped = true;
                rb.AddForce(new Vector2(horizontalForce * direction, jumpForce));
            }
        }
    }
}
