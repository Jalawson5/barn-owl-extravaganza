using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{    
    //Weapon Speed constants//
    public const float MediumSpeed = 0.75f;
    public const float FastSpeed = 0.5f;
    public const float SlowSpeed = 1f;
    
    //Hitbox Prefabs//
    public static GameObject shortCleaveBox;
    public static GameObject largeCleaveBox;
    public static GameObject verticalCleaveBox;
    public static GameObject aerialCleaveBox;
    public static GameObject shortStabBox;
    public static GameObject longStabBox;
    public static GameObject verticalStabBox;
    public static GameObject diveBox;
    public static GameObject slamBox;
    public static GameObject shortDashBox;
    public static GameObject longDashBox;
    public static GameObject sweepBox;
    public static GameObject radialBox; //It's complicated//
    public static GameObject blastBox;
    public static GameObject upBlastBox;
    public static GameObject downBlastBox;
    public static GameObject pillarBox;
    
    //Implemented Weapons//
   /* public static WeaponEntry onehand; //Swords, Axes, Maces//
    public static WeaponEntry twohand; //Greatsword, Greataxe, Hammer//
    public static WeaponEntry spear;
    public static WeaponEntry staff;
    public static WeaponEntry tome;*/
    public static WeaponType onehand;
    public static WeaponType twohand;
    public static WeaponType spear;
    public static WeaponType staff;
    public static WeaponType tome;
    
    //////////////////////////////////////////////////
    //class AttackEntry                             //
    //Contains values necessary for a primary attack//
    //////////////////////////////////////////////////
    public class AttackEntry
    {
        float baseDamage; //multiplier//
        float duration;
        bool isElemental;
        bool isMagical; //Magical attacks based on INT//
        bool hasProjectile;
        GameObject hitbox;
        GameObject projectile; //null if !hasProjectile//
        
        public AttackEntry(float baseDamage, GameObject hitbox, bool isElemental=false, bool isMagical=false, bool hasProjectile=false, GameObject projectile=null, float duration=0.5f)
        {
            this.baseDamage = baseDamage;
            this.isElemental = isElemental;
            this.isMagical = isMagical;
            this.hasProjectile = hasProjectile;
            this.hitbox = hitbox;
            this.projectile = projectile;
            this.duration = duration;
        }
        
        //Getters//
        public float GetBase()
        {
            return baseDamage;
        }
        
        public bool IsElemental()
        {
            return isElemental;
        }
        
        public bool IsMagical()
        {
            return isMagical;
        }
        
        public bool HasProjectile()
        {
            return hasProjectile;
        }
        
        public GameObject GetHitbox()
        {
            return hitbox;
        }
        
        public GameObject GetProjectile()
        {
            if(hasProjectile)
                return projectile;
            
            else
                return null;
        }
        
        public float GetDuration()
        {
            return duration;
        }
    }
    
    ////////////////////////////////////////////////////////
    //class WeaponEntry                                   //
    //Contains the different attack types of a weapon type//
    ////////////////////////////////////////////////////////
    public class WeaponEntry
    {
        AttackEntry neutral;
        AttackEntry forward;
        AttackEntry downward;
        AttackEntry upward;
        AttackEntry neutralA;
        AttackEntry downwardA;
        AttackEntry upwardA;
        float speed;
        
        bool canCharge;
        AttackEntry shortCharge;
        AttackEntry longCharge;
        
        public WeaponEntry(AttackEntry neutral, AttackEntry forward, AttackEntry downward, AttackEntry upward, AttackEntry neutralA, AttackEntry downwardA, AttackEntry upwardA, 
                           float speed, bool canCharge=false, AttackEntry shortCharge=null, AttackEntry longCharge=null)
        {
            this.neutral = neutral;
            this.forward = forward;
            this.downward = downward;
            this.upward = upward;
            this.neutralA = neutralA;
            this.downwardA = downwardA;
            this.upwardA = upwardA;
            this.speed = speed;
            this.canCharge = canCharge;
            this.shortCharge = shortCharge;
            this.longCharge = longCharge;
        }
        
        //Getters//
        public AttackEntry GetNeutral()
        {
            return neutral;
        }
        
        public AttackEntry GetForward()
        {
            return forward;
        }
        
        public AttackEntry GetDown()
        {
            return downward;
        }
        
        public AttackEntry GetUp()
        {
            return upward;
        }
        
        public AttackEntry GetNeutralA()
        {
            return neutralA;
        }
        
        public AttackEntry GetDownA()
        {
            return downwardA;
        }
        
        public AttackEntry GetUpA()
        {
            return upwardA;
        }
        
        public float GetSpeed()
        {
            return speed;
        }
        
        public bool CanCharge()
        {
            return canCharge;
        }
        
        public AttackEntry GetShortCharge()
        {
            if(canCharge)
                return shortCharge;
            
            else
                return null;
        }
        
        public AttackEntry GetLongCharge()
        {
            if(canCharge)
                return longCharge;
            
            else
                return null;
        }
    }
    
    ////////////////////////////
    //void InitWeapons()      //
    //Initializes weapon types//
    ////////////////////////////
    public static void InitWeapons()
    {
        /*onehand = new WeaponEntry(neutral: new AttackEntry(1f, shortCleaveBox),
                                  forward: new AttackEntry(1f, shortDashBox),
                                  downward: new AttackEntry(1.1f, slamBox, true),
                                  upward: new AttackEntry(1f, verticalCleaveBox),
                                  neutralA: new AttackEntry(0.9f, aerialCleaveBox),
                                  downwardA: new AttackEntry(0.9f, verticalCleaveBox),
                                  upwardA: new AttackEntry(1f, verticalCleaveBox),
                                  speed: MediumSpeed);
            
        twohand = new WeaponEntry(neutral: new AttackEntry(1f, largeCleaveBox),
                                  forward: new AttackEntry(1f, shortDashBox),
                                  downward: new AttackEntry(1.2f, slamBox, true),
                                  upward: new AttackEntry(1f, verticalCleaveBox),
                                  neutralA: new AttackEntry(1f, aerialCleaveBox),
                                  downwardA: new AttackEntry(0.9f, verticalCleaveBox),
                                  upwardA: new AttackEntry(0.9f, verticalCleaveBox),
                                  speed: SlowSpeed);
        
        spear = new WeaponEntry(neutral: new AttackEntry(1f, longStabBox),
                                forward: new AttackEntry(1f, longDashBox, true),
                                downward: new AttackEntry(0.8f, sweepBox),
                                upward: new AttackEntry(1f, verticalStabBox),
                                neutralA: new AttackEntry(0.9f, aerialCleaveBox),
                                downwardA: new AttackEntry(1.2f, diveBox, true),
                                upwardA: new AttackEntry(1f, verticalStabBox),
                                speed: MediumSpeed);
        
        staff = new WeaponEntry(neutral: new AttackEntry(0.9f, shortCleaveBox),
                                forward: new AttackEntry(0.9f, shortDashBox),
                                downward: new AttackEntry(1.1f, slamBox, true, true),
                                upward: new AttackEntry(0.9f, verticalCleaveBox, true),
                                neutralA: new AttackEntry(0.9f, radialBox, true, true),
                                downwardA: new AttackEntry(1f, downBlastBox, true, true),
                                upwardA: new AttackEntry(1f, upBlastBox, true, true),
                                speed: MediumSpeed,
                                canCharge: true,
                                shortCharge: new AttackEntry(1f, shortCleaveBox, true, true, true, null),
                                longCharge: new AttackEntry(1.1f, shortCleaveBox, true, true, true, null));
        
        tome = new WeaponEntry(neutral: new AttackEntry(0.9f, shortStabBox, true, true, true, null),
                               forward: new AttackEntry(1.1f, blastBox, true, true),
                               downward: new AttackEntry(0.8f, sweepBox, true, true),
                               upward: new AttackEntry(1f, pillarBox, true, true),
                               neutralA: new AttackEntry(0.8f, radialBox, true, true),
                               downwardA: new AttackEntry(0.9f, downBlastBox, true, true),
                               upwardA: new AttackEntry(1f, upBlastBox, true, true),
                               speed: SlowSpeed,
                               canCharge: true,
                               shortCharge: new AttackEntry(1f, shortStabBox, true, true, true, null),
                               longCharge: new AttackEntry(1.1f, shortStabBox, true, true, true, null));
                               */
        
    }
}
