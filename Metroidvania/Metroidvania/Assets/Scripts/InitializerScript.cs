using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/////////////////////////////////////////////////////////////////////////
//class InitializerScript : MonoBehavior                               //
//Singleton class handles initialization of various scripts and objects//
/////////////////////////////////////////////////////////////////////////
public class InitializerScript : MonoBehaviour
{
    public static InitializerScript instance;
    
    public bool initOK;
    
    //Weapon Types//
    public WeaponType onehand;
    public WeaponType twohand;
    public WeaponType spear;
    public WeaponType staff;
    public WeaponType tome;
    
    //Classes//
    public PlayerClass warrior;
    public PlayerClass wizard;
    public PlayerClass thief;
    
    public PlayerRace human;
    public PlayerRace elf;
    public PlayerRace dwarf;
    public PlayerRace gnome;
    public PlayerRace unnamed1;
    public PlayerRace unnamed2;
    public PlayerRace unnamed3;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        
        InitAttackData();
        InitClassData();
        InitItems();
        InitSkills();
        InitRaceData();
    }
    
    void Start()
    {
        
    }
    
    ////////////////////////////////////
    //void InitAttackData()           //
    //Initializes the AttackData class//
    ////////////////////////////////////
    private void InitAttackData()
    {
        AttackData.onehand = onehand;
        AttackData.twohand = twohand;
        AttackData.spear = spear;
        AttackData.staff = staff;
        AttackData.tome = tome;
        
        AttackData.InitWeapons();
        
        Debug.Log("InitializerScript: Attack Data Initialization Complete");
    }
    
    private void InitClassData()
    {
        CharacterData.warrior = warrior;
        CharacterData.wizard = wizard;
        CharacterData.thief = thief;
        
        Debug.Log("InitializerScript: Player Class Data Initialization Complete");
    }
    
    private void InitRaceData()
    {
        CharacterData.human = human;
        CharacterData.elf = elf;
        CharacterData.dwarf = dwarf;
        CharacterData.gnome = gnome;
        CharacterData.unnamed1 = unnamed1;
        CharacterData.unnamed2 = unnamed2;
        CharacterData.unnamed3 = unnamed3;
        
        Debug.Log("InitializerScript: Player Race Data Initialization Complete");
    }
    
    private void InitItems()
    {
        ItemData.InitItems();
        
        Debug.Log("InitializerScript: Item Data Initialization Complete");
    }
    
    private void InitSkills()
    {
        SkillData.InitSkills();
        
        Debug.Log("InitializerScript: Skill Data Initialization Complete");
    }
}
