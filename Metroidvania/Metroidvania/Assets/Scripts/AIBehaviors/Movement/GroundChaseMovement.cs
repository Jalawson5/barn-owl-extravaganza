using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChaseMovement : MonoBehaviour
{
    public GameObject targetPlayer;
    private Transform targetTransform;
    
    public float moveSpeed;
    public float aggroRange;
    public bool aggressive;
    public bool ignoreCliffs; //Should this enemy ignore cliffs? True will cause the enemy to walk off cliffs to the ground below.//
    public bool canJump; //Should this enemy jump to reach the player?//
    public float jumpRange; //Distance from the player where the enemy should jump//
    public float jumpForce; //Force at which the enemy should jump//
    public float jumpTimerMax; //How often can the enemy jump?//
    
    public EnemyBehavior parent;
    
    private int direction;
    private bool aggroed;
    private bool canMove;
    private bool grounded;
    
    private float jumpTimer;
    private float height;
    private float width;
    
    private LayerMask terrainLayer;
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = targetPlayer.transform;
        
        direction = -1;
        parent.SetDirection(-1);
        aggroed = false;
        canMove = false;
        grounded = false;
        
        jumpTimer = 0f;
        
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
        
        RaycastHit2D hitEdge;
        RaycastHit2D hitWall;
        
        if(direction == -1)
        {
            hitEdge = Physics2D.Raycast(transform.position - new Vector3(width, height, 0), Vector2.down, 0.1f, terrainLayer);
                
            hitWall = Physics2D.Raycast(transform.position - new Vector3(width, 0, 0), Vector2.left, 0.1f, terrainLayer);
        }
            
        else
        {
            hitEdge = Physics2D.Raycast(transform.position + new Vector3(width, height * (-1), 0), Vector2.down, 0.1f, terrainLayer);
                
            hitWall = Physics2D.Raycast(transform.position + new Vector3(width, 0, 0), Vector2.right, 0.1f, terrainLayer);
        }
        
        if((!ignoreCliffs && hitEdge.collider == null) || hitWall.collider != null)
        {
            canMove = false;
        }
        
        else
        {
            canMove = true;
        }
        
        if(ignoreCliffs || canJump)
        {
            RaycastHit2D groundLeft;
            RaycastHit2D groundRight;
            
            groundLeft = Physics2D.Raycast(transform.position - new Vector3(width, height, 0), Vector2.down, 0.1f, terrainLayer);
            groundRight = Physics2D.Raycast(transform.position + new Vector3(width, height * (-1), 0), Vector2.down, 0.1f, terrainLayer);
            
            if(groundLeft.collider != null || groundRight.collider != null)
                grounded = true;
                
            else
                grounded = false;
        }
        
        if(canJump && jumpTimer < jumpTimerMax)
        {
            jumpTimer += Time.deltaTime;
        }
        
        if(aggroed && canMove)
        {
            rb.velocity = new Vector3(moveSpeed * direction, rb.velocity.y, 0);
        }
        
        if(aggroed && canJump && grounded && parent.GetDistance() <= jumpRange && targetTransform.position.y > transform.position.y && jumpTimer >= jumpTimerMax)
        {
            Debug.Log("Jump! Jump!");
            jumpTimer = 0f;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }
}
