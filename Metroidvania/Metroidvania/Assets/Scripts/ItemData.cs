using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////////////////////////////
//class ItemData                                               //
//Loads item data into several arrays to be accessed at runtime//
/////////////////////////////////////////////////////////////////
public class ItemData
{
    private static Weapon[] weapons;
    private static Armor[] armors;
    private static Accessory[] accessories;
    private static UseItem[] items;
    
    /////////////////////////////////////////////////////////
    //void InitItems()                                     //
    //Initializes the item database, including equipment   //
    //Sorts each array by ID, for easier searching later on//
    /////////////////////////////////////////////////////////
    public static void InitItems()
    {
        weapons = Resources.LoadAll<Weapon>("Weapons");
        armors = (Armor[])Resources.LoadAll<Armor>("Armors");
        accessories = (Accessory[])Resources.LoadAll<Accessory>("Accessories");
        items = (UseItem[])Resources.LoadAll<UseItem>("UseItems");
        
        Array.Sort(weapons, delegate(Weapon x, Weapon y){return (x.id < y.id)?1:0;});
        Array.Sort(armors, delegate(Armor x, Armor y){return (x.id < y.id)?1:0;});
        Array.Sort(accessories, delegate(Accessory x, Accessory y){return (x.id < y.id)?1:0;});
        Array.Sort(items, delegate(UseItem x, UseItem y){return (x.id < y.id)?1:0;});
    }
    
    public static Weapon GetWeapon(int id)
    {
        return weapons[id];
    }
    
    public static Armor GetArmor(int id)
    {
        return armors[id];
    }
    
    public static Accessory GetAccessory(int id)
    {
        return accessories[id];
    }
    
    public static UseItem GetUseItem(int id)
    {
        return items[id];
    }

}
