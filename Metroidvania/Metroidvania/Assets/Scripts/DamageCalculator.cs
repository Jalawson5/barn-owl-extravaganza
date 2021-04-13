using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles damage calculations//
public class DamageCalculator
{
    private const int BaseCrit = 1;
    private const int BaseAcc = 90;
    
    ///////////////////////////////////////////////////////////////
    //int CalculatePhysicalDamage(int, int, int, int)            //
    //Calculates physical damage and returns the result          //
    //int attackSTR - The attacker's Strength stat               //
    //int weaponSTR - The attacker's Weapon                      //
    //float attackBase - The base attack multiplier of the attack//
    //int defense - The defender's defense stat                  //
    ///////////////////////////////////////////////////////////////
    public static int CalculatePhysicalDamage(int attackStat, float attackBase, int defense)
    {
        int total = (int)(attackStat * attackBase) - defense;
        
        if(total < 0)
        {
            total = 0;
        }
        
        System.Random rand = new System.Random();
        
        int plusMinus = rand.Next(2);
        
        if(plusMinus == 0)
        {
            total -= (int)(total * rand.NextDouble() * 0.1);
        }
        
        else
        {
            total += (int)(total * rand.NextDouble() * 0.1);
        }
        
        return total;
    }
    
    ///////////////////////////////////////////////////////////////
    //int CalculateMagicalDamage(int, int, int, int)             //
    //Calculates magical damage and returns the result           //
    //int attackINT - The attacker's Intellect stat              //
    //int weaponINT - The attacker's Weapon attack               //
    //float attackBase - The base attack multiplier of the attack//
    //int mdefense - The defender's magic defense stat           //
    ///////////////////////////////////////////////////////////////
    public static int CalculateMagicalDamage(int mattackStat, float attackBase, int mdefense)
    {
        int total = (int)(mattackStat * attackBase) - mdefense;
        
        if(total < 0)
        {
            total = 0;
        }
        
        System.Random rand = new System.Random();
        
        int plusMinus = rand.Next(2);
        
        if(plusMinus == 0)
        {
            total -= (int)(total * rand.NextDouble() * 0.1);
        }
        
        else
        {
            total += (int)(total * rand.NextDouble() * 0.1);
        }
        
        return total;
    }
    
    /////////////////////////////////////////////////////////////////////
    //bool Critical(int)                                               //
    //calculates if an attack is a critical                            //
    //With high enough luck (990+) it is possible to have a 100% chance//
    //int attackLUCK - The attacker's Luck stat                        //
    /////////////////////////////////////////////////////////////////////
    public static bool Critical(int attackLUCK)
    {
        int chance = (int)(BaseCrit + (attackLUCK / 10));
        
        if(new System.Random().NextDouble() * 100 < chance)
            return true;
        
        return false;
    }
    
    ///////////////////////////////////////////////
    //bool Hits(int, int)                        //
    //Calculates if an attack hits or not        //
    //Base Hit% is 90%                           //
    //int attackAGI - The attacker's Agility stat//
    //int defendAGI - The defender's Agility stat//
    ///////////////////////////////////////////////
    public static bool Hits(int attackAGI, int defendAGI)
    {
        int chance = (int)(BaseAcc + ((attackAGI / 10) - (attackAGI / 10)));
        
        if(new System.Random().NextDouble() * 100 < chance)
            return true;
        
        return false;
    }
}
