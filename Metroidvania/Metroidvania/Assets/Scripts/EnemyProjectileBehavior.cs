using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBehavior : MonoBehaviour
{
    public float projectileSpeed;
    public float lifetime;
    public bool infinitePierce; //If true, projectile will never be destroyed on contact with an enemy//
    public bool throughWalls; //If true, projectile will not be destroyed on contact with terrain//
    public int pierceCount; //Ignored if infinitePierce = true//

    public EnemyEntry parentStats;
    
    private float direction;
    private bool moving;
    private Rigidbody2D rb;
    
    private float baseAttack;
    private bool isElemental;
    private bool isMagical;
    
    private LayerMask enemyLayer;
    private LayerMask terrainLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemyLayer = LayerMask.GetMask("Enemy");
        terrainLayer = LayerMask.GetMask("Solid");
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            //rb.velocity = new Vector2(projectileSpeed * direction, 0);
            lifetime -= Time.deltaTime;
        }
        
        if(lifetime <= 0)
            Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 10) //Player layer//
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(parentStats, baseAttack, isMagical);
            
            if(!infinitePierce)
            {
                pierceCount--;
            }
            
            if(pierceCount <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        if(other.gameObject.layer == 8) //Solid layer//
        {
            if(!throughWalls)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void Attack(int direction)
    {
        this.direction = direction;
        moving = true;
    }
    
    public void InitStats(float baseAttack, bool isElemental, bool isMagical)
    {
        this.baseAttack = baseAttack;
        this.isElemental = isElemental;
        this.isMagical = isMagical;
    }
}
