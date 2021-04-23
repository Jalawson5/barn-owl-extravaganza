using System.Collections;
using System.Collections.Generic;
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
    
    private Rigidbody2D rb;
    
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
    
    // Start is called before the first frame update
    void Start()
    {
        //jumpSpeed = 16.5f;
        jumpTime = 0f;
        jumpTimeMax = 0.7f;
        grounded = false;
        
        moveSpeed = 5f;
        dashSpeed = 7f;
        moving = false;
        direction = 1;
        dashing = false;
        dashTimer = 0f;
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        
        weaponType = currWeapon.type;
        weaponTimerMax = weaponType.speed;
        weaponTimer = 0f;
        charging = false;
        chargeTimer = 0f;
        chargeTimeMax = 2f;
        shortCharge = 1f;
        
        terrainLayer = LayerMask.GetMask("Solid");
        
        stats = new CharacterData.CharacterEntry(CharacterData.warrior, "Dummy");
        
        //skill1 = (ActionSkill)(stats.GetSkill1().GetSkill());
        skill2 = (ActionSkill)(stats.GetSkill2().GetSkill());
        
        maxHP = stats.GetHP(true);
        maxMP = stats.GetMP(true);
        currentHP = maxHP;
        currentMP = maxMP;
        iframes = 1f;
        iframesTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Is the player on the ground?//
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(-0.5f, -0.4f, 0), Vector2.down, 0.2f, terrainLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(0.5f, -0.4f, 0), Vector2.down, 0.2f, terrainLayer);
        
        
        if(hitLeft.collider == null && hitRight.collider == null)
        {
            grounded = false;
        }
        
        else
        {
            grounded = true;
        }
        
        //Jump Controls//
        if(Input.GetKeyDown("z") && grounded && !attacking)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumping = true;
        }
        
        if(jumping && (jumpTime > jumpTimeMax || Input.GetKeyUp("z")) && rb.velocity.y >= 0)
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
        
        else if((Input.GetKeyDown("left") || Input.GetKeyDown("right")) && dashTimer > 0)
        {
            dashing = true;
        }
        
        if(dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
        
        if(Input.GetKey("left"))
        {
            moving = true;
            direction = -1;
        }
        
        else if(Input.GetKey("right"))
        {
            moving = true;
            direction = 1;
        }
        
        else
        {
            moving = false;
            dashing = false;
        }
        
        if(moving && !attacking)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0.50f * direction, 0, 0), (direction == -1)?Vector2.left:Vector2.right, 0.1f, terrainLayer);
            RaycastHit2D hitFeet = Physics2D.Raycast(transform.position + new Vector3(0.50f * direction, -0.5f, 0), (direction == -1)?Vector2.left:Vector2.right, 0.1f, terrainLayer);
            
            if(hit.collider == null && hitFeet.collider == null)
            {
                if(dashing)
                    rb.velocity = new Vector2(dashSpeed * direction, rb.velocity.y);
                    
                else
                    rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
            }
        }
        
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        
        //Attack controls//
        if(weaponTimer < weaponTimerMax)
            weaponTimer += Time.deltaTime;
            
        if(attackDuration > 0)
            attackDuration -= Time.deltaTime;
        
        if(attackDuration <= 0)
            attacking = false;
        
        if((Input.GetKeyDown("x") && weaponTimer >= weaponTimerMax) || (charging && Input.GetKeyUp("x")))
        {
            weaponTimer = 0f;
            AttackEntry attack = null;
            GameObject temp = null;
            
            
            
            if(Input.GetKeyUp("x") && charging)
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
        
        if(Input.GetKey("x") && weaponType.canCharge && !charging && weaponTimer >= weaponTimerMax)
        {
            charging = true;
            chargeTimer = chargeTimeMax;
        }
        
        if(Input.GetKey("x") && charging)
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
        if(Input.GetKeyDown("a") && !charging && weaponTimer >=weaponTimerMax)
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
        hpBar.AdjustBar(currentHP, maxHP);
        
        //Magic//
        mpBar.AdjustBar(currentMP, maxMP);
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
    
    ////////////////////////////////////////////////
    //void HandleDeath()                          //
    //Handles player death. Specific mechanics TBD//
    ////////////////////////////////////////////////
    private void HandleDeath()
    {
        Debug.Log("You got dead :(");
    }
}
