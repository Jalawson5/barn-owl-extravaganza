using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRoamMovement : MonoBehaviour
{
    public GameObject targetPlayer;
    private Transform targetTransform;
    
    public EnemyBehavior parent;

    public float moveSpeed;
    public bool wander; //wander back and forth? Otherwise, move to end of platform and turn around//
    public float wanderTimer; //if wander is true, how often should this enemy turn?//
    public bool facePlayer; //should this enemy always face the player?//
    
    private int direction;
    private float moveTimer;
    private Rigidbody2D rb;
    
    private float height;
    private float width;
    
    private bool isMoving; //To be used with attack scripts//
    
    private LayerMask terrainLayer;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = targetPlayer.transform;
        
        direction = -1;
        parent.SetDirection(-1);
        moveTimer = 0;
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        height = gameObject.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        width = gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        
        terrainLayer = LayerMask.GetMask("Solid");
        
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if direction should change//
        if(wander)
        {
            moveTimer += Time.deltaTime;
            
            if(moveTimer >= wanderTimer)
            {
                direction *= -1;
                parent.SetDirection(direction);
                moveTimer = 0;
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
        
        if(hitEdge.collider == null || hitWall.collider != null)
        {
            direction *= -1;
            if(wander)
            {
                moveTimer = 0;
            }
        }
        
        if(isMoving)
        {
            rb.velocity = new Vector3(moveSpeed * direction, rb.velocity.y, 0);
        }
    }
}
