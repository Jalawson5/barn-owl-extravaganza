using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Jump Related Variables//
    [SerializeField]
    private float jumpSpeed; //How fast to jump//
    private float jumpTime; //How long the player has been jumping//
    private float jumpTimeMax; //How long the player can jump//
    private bool jumping; //Is the player currently jumping?//
    private bool grounded; //Is the player currently on the ground?//
    
    //Horizontal Movement Variables//
    private float moveSpeed; //How fast to move//
    private float dashSpeed; //How fast to move while dashing//
    private bool moving; //Is the player currently moving (horizontal)?//
    private int direction; //What direction the player is facing (left = -1, right = 1)//
    private bool dashing; //Is the player currently dashing?//
    private float dashTimer; //How fast the player must double-tap move keys to dash//
    private bool changeDirection; //Has the player changed direction this frame?//
    
    //Crouching and Sliding Variables//
    private bool crouching; //Is the player currently crouching?//
    private bool canStand; //Does the player have room to stand?//
    private bool sliding; //Is the player currently sliding?//
    private float slideForce; //Force of the player's slide//
    private float slideTimer; //Time spent sliding//
    private float slideTimerMax; //Maximum slide time//
    
    //Underwater variables//
    private bool underwater; //Is the player underwater?//
    private float waterDrag; //Force multiplier for being underwater//
    
    //////////////////////////////////////////////////////////////
    //Note about player states:                                 //
    //The player cannot move while crouching                    //
    //The player can jump while crouching, cancelling the crouch//
    //Crouching while moving cancels movement                   //
    //////////////////////////////////////////////////////////////
    
    //Attacking and Weapon Variables//
    private WeaponType weaponType; //The type of weapon that is currently equipped//
    private float weaponTimerMax; //How long the player must wait between attacks//
    private float weaponTimer; //Timer for attack cooldown//
    private bool attacking; //Is the player currently attacking? Player cannot move while attacking on the ground//
    private float attackDuration; //Timer for the current attack//
    private bool charging; //Is the player charging an attack?//
    private float chargeTimer; //How long the player has been charging an attack//
    private float chargeTimeMax; //How long the player must charge to achieve a full charge//
    private float shortCharge; //How long the player must charge to achieve a short charge//
    
    //Status Variables//
    private int maxHP;
    private int maxMP;
    private int currentHP; //Current health value. Die when current HP reaches 0//
    private int currentMP; //Current MP value//
    private float iframes; //Length of iframes//
    private float iframesTimer; //Timer for iframes//
    
    private LayerMask terrainLayer; //Layer of solid ground//

    public CharacterData.CharacterEntry stats; //Stats of the current character//
    public Weapon currWeapon; //Current equipped weapon//
    public ActionSkill skill1; //Skill equipped in slot 1//
    public ActionSkill skill2; //Skill equipped in slot 2//
    
    public UIBarBehavior hpBar;
    public UIBarBehavior mpBar;
    
    //Ability Variables//
    private bool hasDoubleJump;
    private bool hasWallJump;
    private bool hasRockBreaker;
    private bool hasSlide;
    private bool hasSwim;
    private bool hasKey;
    
    private bool jumpAgain;
    private bool againstWall;
    private bool wallJumped;
    private int wallJumpFrames; //Number of frames during which a wall-jump can be performed//
    private int wallDirection;
    
    //Physics Variables//
    private float gravity;
    private float height;
    private float width;
    
    [SerializeField]
    private float wallSlide;
    
    [SerializeField]
    private float accelConst; //Acceleration Constant helps determine how fast for the player to accelerate.//
    
    //References to other Components and Objects//
    public MasterController master;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        master = MasterController.instance;
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<BoxCollider2D>();
    
        //jumpSpeed = 16.5f;
        jumpTime = 0f;
        jumpTimeMax = 0.7f;
        grounded = false;
        
        moveSpeed = 3f;
        dashSpeed = 5f;
        moving = false;
        direction = 1;
        dashing = false;
        dashTimer = 0f;
        changeDirection = false;
        
        crouching = false;
        canStand = true;
        sliding = false;
        slideForce = 5f;
        slideTimer = 0f;
        slideTimerMax = 0.5f;
        
        weaponType = currWeapon.type;
        weaponTimerMax = weaponType.speed;
        weaponTimer = 0f;
        charging = false;
        chargeTimer = 0f;
        chargeTimeMax = 2f;
        shortCharge = 1f;
        
        underwater = false;
        waterDrag = 0.5f;
        
        terrainLayer = LayerMask.GetMask("Solid");
        
        stats = new CharacterData.CharacterEntry(CharacterData.human, CharacterData.warrior, false, "Dummy");
        
        //skill1 = (ActionSkill)(stats.GetSkill1().GetSkill());
        skill2 = (ActionSkill)(stats.GetSkill2().GetSkill());
        
        maxHP = stats.GetHP(true);
        maxMP = stats.GetMP(true);
        currentHP = maxHP;
        currentMP = maxMP;
        iframes = 1f;
        iframesTimer = 1f;
        
        hasDoubleJump = true; //stats.HasDoubleJump();
        hasWallJump = true; //stats.HasWallJump();
        hasRockBreaker = true; //stats.HasRockBreaker();
        hasSlide = true; //stats.HasSlide();
        hasSwim = true; //stats.HasSwim();
        hasKey = stats.HasKey();
        
        jumpAgain = false;
        againstWall = false;
        wallJumped = false;
        wallJumpFrames = 5;
        wallDirection = direction;
        
        height = col.bounds.size.y / 2;
        width = col.bounds.size.x / 2;
        
        gravity = 10f;
        
        CharacterData.currentChar = stats;
    }

    // Update is called once per frame
    void Update()
    {
        if(master.isPaused)
            return;
            
        float drag = underwater?waterDrag:1f; //If player is underwater, apply waterDrag//
            
        if(!grounded)
        {
            if(againstWall && hasWallJump && moving)
                rb.velocity -= new Vector2(0, wallSlide * Time.deltaTime * drag);

            else
                rb.velocity -= new Vector2(0, gravity * Time.deltaTime * drag);
                
            sliding = false;
            slideTimer = 0f;
        }
        
        else if(!jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    
        //Is the player on the ground?//
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(width * -1, height * -1, 0), Vector2.down, 0.05f, terrainLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(width, height * -1, 0), Vector2.down, 0.05f, terrainLayer);
        
        
        if(hitLeft.collider == null && hitRight.collider == null)
        {
            grounded = false;
        }
        
        else
        {
            grounded = true;
            
            if(hasDoubleJump)
                jumpAgain = true;
        }
        
        //Jump Controls//
        if(Input.GetKeyDown(master.controls.GetJumpKey()) && (grounded || jumpAgain || (againstWall && hasWallJump)) && !attacking)
        {
            if(grounded && (!crouching || (crouching && !hasSlide)))
                rb.AddForce(Vector2.up * jumpSpeed * drag, ForceMode2D.Impulse);
            
            else if(crouching && hasSlide)
            {
                sliding = true;
            }
                
            else if(hasWallJump && wallJumpFrames > 0)
            {
                rb.velocity = new Vector2(0f, 0f);
                rb.AddForce(new Vector2(jumpSpeed * -1.5f * wallDirection * drag, jumpSpeed * 0.7f * drag), ForceMode2D.Impulse);
                Debug.Log("Force: " + (jumpSpeed * -1.5f * wallDirection * drag));
                Debug.Log("Normal Force: " + (jumpSpeed * -1.5f * wallDirection));
                wallJumped = true;
                wallJumpFrames = 0;
                Debug.Log("Jump");
            }    
            
            else if(hasSwim && underwater)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpSpeed * 0.7f * drag, ForceMode2D.Impulse);
            }
            
            else if(jumpAgain)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * jumpSpeed * 0.7f * drag, ForceMode2D.Impulse); //Slightly weaker jump when double jumping//
                jumpAgain = false;
            }
            
            if(!sliding)
                jumping = true;
        }
        
        if(jumping && (jumpTime > jumpTimeMax || Input.GetKeyUp(master.controls.GetJumpKey())) && rb.velocity.y >= 0)
        {
            jumping = false;
            jumpTime = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        
        else if(jumping)
        {
            jumpTime += Time.deltaTime;
        }
        
        //Horizontal Movement Controls//
        if((Input.GetKeyDown("left") || Input.GetKeyDown("right")) && dashTimer <= 0)
        {
            dashTimer = 0.3f;
        }
        
        else if((Input.GetKeyDown("left") || Input.GetKeyDown("right")) && dashTimer > 0 && grounded)
        {
            dashing = true;
        }
        
        if(dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
        
        //Crouching and Sliding//
        if(crouching)
        {
            hitLeft = Physics2D.Raycast(transform.position + new Vector3(width * -1, height / 2f, 0), Vector2.up, 1.2f,  terrainLayer);
            hitRight = Physics2D.Raycast(transform.position + new Vector3(width, height / 2f, 0), Vector2.up, 1.2f, terrainLayer);
            
            if(hitLeft.collider != null || hitRight.collider != null)
                canStand = false;
                
            else
                canStand = true;
        }
        
        if(Input.GetKey("down") && grounded)
        {
            if(!crouching)
            {
                col.size = new Vector3(col.size.x, col.size.y / 2);
                col.offset = new Vector2(col.offset.x, col.size.y / -2);
            }
            
            crouching = true;
        }
        
        else if(canStand)
        {
            if(crouching)
            {
                col.size = new Vector3(col.size.x, col.size.y * 2);
                col.offset = new Vector2(col.offset.x, 0);
            }
            
            crouching = false;
            sliding = false;
            slideTimer = 0f;
        }
        
        if(sliding)
        {
            rb.AddForce(new Vector2(slideForce * direction, 0f), ForceMode2D.Impulse);
            slideTimer += Time.deltaTime;
        }
        
        if(slideTimer >= slideTimerMax)
        {
            sliding = false;
            slideTimer = 0f;
        }
        
        if(Input.GetKey("left"))
        {
            if(!crouching)
                moving = true;
            
            if(direction == 1)
                changeDirection = true;
                
            direction = -1;
        }
        
        else if(Input.GetKey("right"))
        {
            if(!crouching)
                moving = true;
            
            if(direction == -1)
                changeDirection = true;
                
            direction = 1;
        }
        
        else
        {
            moving = false;
            dashing = false;
        }
        
        if(moving && !attacking)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(width * direction, 0, 0), (direction == -1)?Vector2.left:Vector2.right, 0.05f, terrainLayer);
            RaycastHit2D hitFeet = Physics2D.Raycast(transform.position + new Vector3(width * direction, -0.5f, 0), (direction == -1)?Vector2.left:Vector2.right, 0.05f, terrainLayer);
            if(hit.collider == null && hitFeet.collider == null)
            {
                if(changeDirection)
                {
                    if((direction == -1 && rb.velocity.x > 0) || (direction == 1 && rb.velocity.x < 0))
                        rb.velocity = new Vector2(0, rb.velocity.y);
                }
                
                if(dashing)
                {
                    //rb.velocity = new Vector2(dashSpeed * direction, rb.velocity.y);
                    if((direction == -1 && rb.velocity.x > dashSpeed * -1) || (direction == 1 && rb.velocity.x < dashSpeed))
                    {
                        rb.AddForce(new Vector2(dashSpeed * accelConst * direction, 0) * drag, ForceMode2D.Impulse);
                    }
                }
                
                else
                {
                    //rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
                    if((direction == -1 && rb.velocity.x > moveSpeed * -1) || (direction == 1 && rb.velocity.x < moveSpeed))
                    {
                        rb.AddForce(new Vector2(moveSpeed * accelConst * direction, 0) * drag, ForceMode2D.Impulse);
                    }
                }
                
                if(againstWall)
                {
                    wallJumpFrames = 40;
                }
                
                againstWall = false;
            }
            
            else
            {
                againstWall = true;
                wallJumpFrames = 0;
                wallDirection = direction;
            }
        }
        
        else if(!sliding)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            againstWall = false;
        }
        
        if(wallJumpFrames > 0)
        {
            wallJumpFrames--;
        }
        
        //Attack controls//
        if(weaponTimer < weaponTimerMax)
            weaponTimer += Time.deltaTime;
            
        if(attackDuration > 0)
            attackDuration -= Time.deltaTime;
        
        if(attackDuration <= 0)
            attacking = false;
        
        if((Input.GetKeyDown(master.controls.GetAttackKey()) && weaponTimer >= weaponTimerMax) || (charging && Input.GetKeyUp(master.controls.GetAttackKey())))
        {
            weaponTimer = 0f;
            AttackEntry attack = null;
            GameObject temp = null;
            
            
            
            if(Input.GetKeyUp(master.controls.GetAttackKey()) && charging)
            {
                //Long Charge//
                if(chargeTimer <= 0)
                {
                    attack = weaponType.longCharge;
                }
                
                //Short Charge//
                else if(chargeTimer <= shortCharge)
                {
                    attack = weaponType.shortCharge;
                }
                
                //Too early, no charge//
                else
                {
                    attack = weaponType.neutral;
                }
                
                temp = Instantiate(attack.hitbox, transform, false);
                temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                
                charging = false;
            }
            
            //Ground Attacks//
            else if(grounded)
            {            
                //Forward or Dash Attack//
                if(dashing)
                {
                    attack = weaponType.forward;
                    temp = Instantiate(attack.hitbox, transform, false);
                    temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                    temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                    attackDuration = attack.duration;
                }
                
                //Upward Attack//
                else if(Input.GetKey("up"))
                {
                    attack = weaponType.upward;
                    temp = Instantiate(attack.hitbox, transform, false);
                    temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                    temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                    attackDuration = attack.duration;
                }
                
                //Downward Attack//
                else if(Input.GetKey("down"))
                {
                    attack = weaponType.downward;
                    temp = Instantiate(attack.hitbox, transform, false);
                    temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                    temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                    attackDuration = attack.duration;
                }
                
                //Neutral Attack//
                else
                {
                    
                    attack = weaponType.neutral;
                    temp = Instantiate(attack.hitbox, transform, false);
                    temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                    temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                    attackDuration = attack.duration;
                }
                
                attacking = true;
            }
            
            //Aerial Attacks//
            else
            {
                //Upward Air Attack//
                if(Input.GetKey("up"))
                {
                    attack = weaponType.upwardA;
                    temp = Instantiate(attack.hitbox, transform, false);
                    temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                    temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                }
                
                //Downward Air Attack//
                else if(Input.GetKey("down"))
                {
                    attack = weaponType.downwardA;
                    temp = Instantiate(attack.hitbox, transform, false);
                    temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                    temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                }
                
                //Neutral Air Attack (Triggers regardless of horizontal movement//
                else
                {
                    attack = weaponType.neutralA;
                    temp = Instantiate(attack.hitbox, transform, false);
                    temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                    temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                }
            }
            
            
            
            if(attack != null && attack.hasProjectile)
            {
                GameObject proj = Instantiate(attack.projectile, new Vector2(temp.transform.position.x, temp.transform.position.y), Quaternion.identity);
                BasicProjectileBehavior projScript = proj.GetComponent<BasicProjectileBehavior>();
                projScript.Attack(direction);
                projScript.InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                projScript.parentStats = stats;
            }
        }
        
        if(Input.GetKey(master.controls.GetAttackKey()) && weaponType.canCharge && !charging && weaponTimer >= weaponTimerMax)
        {
            charging = true;
            chargeTimer = chargeTimeMax;
        }
        
        if(Input.GetKey(master.controls.GetAttackKey()) && charging)
        {
            Debug.Log("Charging");
            
            if(chargeTimer > 0)
                chargeTimer -= Time.deltaTime;
                
            if(chargeTimer <= 0)
                Debug.Log("Long Charge Complete");
                
            else if(chargeTimer <= shortCharge)
                Debug.Log("Short Charge Complete");
        }
        
        //Skill Controls//
        //Action skills of all types share an attack cooldown with normal attacks//
        //Cannot use Action Skills of any type while charging an attack//
        
        //Skill 1//
        if(Input.GetKeyDown(master.controls.GetSkillOneKey()) && !charging && weaponTimer >= weaponTimerMax)
        {            
            Debug.Log("Skill: " + skill1.name);
            Debug.Log("MP: " + currentMP);
            Debug.Log("Cost: " + skill1.cost);
            if(skill1 != null && currentMP >= skill1.cost)
            {
                weaponTimer = 0f;
                AttackEntry attack = null;
                GameObject temp = null;
                
                if(skill1.target == SkillData.TargetType.Self)
                {
                    //Do the thing//
                }
                
                else if(skill1.target == SkillData.TargetType.Enemy)
                {
                    //attack = ((AttackAction)skill1).attack;
                    AttackAction tempAction = (AttackAction)skill1;
                    attack = tempAction.attack;
                    temp = Instantiate(attack.hitbox, transform, false);
                    temp.GetComponent<AttackHitboxBehavior>().Attack(attack.duration, direction);
                    temp.GetComponent<AttackHitboxBehavior>().InitStats(attack.baseAttack, attack.isElemental, attack.isMagical);
                }
                
                else if(skill1.target == SkillData.TargetType.Ally)
                {
                    //Do the other thing//
                }
                
                currentMP -= skill1.cost;
                if(currentMP < 0)
                    currentMP = 0;
            }
        }
        
        //Check iframes//
        if(iframesTimer < iframes)
        {
            iframesTimer += Time.deltaTime;
        }
        
        //UI Updates//
        
        //Health//
        if(hpBar != null)
            hpBar.AdjustBar(currentHP, maxHP);
        
        //Magic//
        if(mpBar != null)
            mpBar.AdjustBar(currentMP, maxMP);
        
        //Reset any single-frame variables//
        changeDirection = false;
        wallJumped = false;
        
        //If velocity.x is extremely small, ignore velocity.x//
        if(Math.Abs(rb.velocity.x) < 0.1f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        
        if(Math.Abs(rb.velocity.x) > 7f)
        {
            rb.velocity = new Vector2(7f * direction, rb.velocity.y);
        }
    }
    
    ////////////////////////////////////////
    //int GetDirection()                  //
    //Returns the direction of this player//
    //Used by other scripts               //
    ////////////////////////////////////////
    public int GetDirection()
    {
        return direction;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //void TakeTouchDamage(EnemyEntry)                                                                                   //
    //Calculates enemy touch damage (contact with the enemy hitbox)                                                      //
    //For balancing difficulty, I am considering allowing only certain enemies to crit, so LUCK is not considered for now//
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void TakeTouchDamage(EnemyEntry otherStats)
    {
        if(iframesTimer < iframes)
        {
            //iframes are active, take no damage//
            return;
        }
        
        if(DamageCalculator.Hits(otherStats.enemyAGI, stats.GetAGI()))
        {
            int damage = DamageCalculator.CalculatePhysicalDamage(otherStats.enemyATK, 1f, stats.GetDEF());
            
            currentHP -= damage;
            
            if(currentHP <= 0)
            {
                currentHP = 0;
                HandleDeath();
            }
        }
        
        iframesTimer = 0;
    }
    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //void TakeDamage(EnemyEntry, float, bool)                                                                           //
    //Calculates damage from an enemy attack                                                                             //
    //For balancing difficulty, I am considering allowing only certain enemies to crit, so LUCK is not considered for now//
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void TakeDamage(EnemyEntry otherStats, float baseDamage, bool isMagical)
    {
        if(iframesTimer < iframes)
        {
            //iframes are active, take no damage//
            return;
        }
    
        int damage = 0;
        if(DamageCalculator.Hits(stats.GetAGI(), otherStats.enemyAGI))
        {
            if(isMagical)
            {
                damage = DamageCalculator.CalculateMagicalDamage(otherStats.enemyMATK, baseDamage, stats.GetMDEF());
            }
            
            else
            {
                damage = DamageCalculator.CalculatePhysicalDamage(otherStats.enemyATK, baseDamage, stats.GetDEF());
            }
        }
        
        currentHP -= damage;
        
        if(currentHP <= 0)
        {
            currentHP = 0;
            HandleDeath();
        }
        
        iframesTimer = 0;
    }
    
    ////////////////////////////////////////////////////////
    //bool CanBreak()                                     //
    //Returns true if the player has hasRockBreaker = true//
    ////////////////////////////////////////////////////////
    public bool CanBreak()
    {
        return hasRockBreaker;
    }
    
    ////////////////////////////////////////////////
    //void HandleDeath()                          //
    //Handles player death. Specific mechanics TBD//
    ////////////////////////////////////////////////
    private void HandleDeath()
    {
        Debug.Log("You got dead :(");
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {Debug.Log("Trigger Entered");
        if(other.gameObject.layer == 15)
        {
            underwater = true;
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 15)
        {
            underwater = false;
        }
    }
    
    
}
