using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Stats:                                                                                                                                   //
    //HP is your health.                                                                                                                       //
    //MP is your magic points. Skills cost MP.                                                                                                 //
    //STR is your strength, which affects the power of non-magical primary attacks and skills.                                                 //
    //INT is your intellect, which affects the power of magical attacks and skills, and adds bonus damage to all skills.                       //
    //END is your Endurance, which provides some natural damage reduction and provides a bonus to your max health.                             //
    //SPR is your Spirit, which provides natural magic resistance and affects the power of healing skills. Provides a bonus to MP regeneration.//
    //AGI is your Agility, which affects accuracy and dodge rate. Also provides a small damage bonus to critical hits.                         //
    //LUK is your Luck, which affects drops from enemies as well as the chance of landing a critical hit.                                      //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    //Classes//
    public const int NumClasses = 3;
    
    public static PlayerClass warrior;
    public static PlayerClass wizard;
    public static PlayerClass thief;
    
    //Races//
    public const int NumRaces = 7;
    
    public static PlayerRace human;
    public static PlayerRace elf;
    public static PlayerRace dwarf;
    public static PlayerRace gnome;
    public static PlayerRace unnamed1;
    public static PlayerRace unnamed2;
    public static PlayerRace unnamed3;
    
    //Stat Conversion Rates//
    public static float ENDtoHP = 1.5f;
    public static float SPRtoMP = 1.5f;
    public static float STRtoATK = 0.8f;
    public static float INTtoMATK = 0.8f;
    public static float ENDtoDEF = 0.5f;
    public static float SPRtoMDEF = 0.5f;
    
    public class CharacterEntry
    {
        PlayerRace playerRace;
        PlayerClass playerClass;
        string name;
        
        int level;
        int exp;
        
        //Character Stats without equipment or skills//
        int playerHP;
        int playerMP;
        int playerSTR;
        int playerINT;
        int playerEND;
        int playerSPR;
        int playerAGI;
        int playerLUCK;
        
        //IDs of equipped items//
        int weapon;
        int armor;
        int accessory1;
        int accessory2;
        int rune1;
        int rune2;
        
        //Bonus stats from skills//
        int skillSTR;
        int skillINT;
        int skillEND;
        int skillSPR;
        int skillAGI;
        int skillLUCK;
        
        //Equipped Skills//
        SkillNode skill1;
        SkillNode skill2;
        
        //List of learned skills//
        List<SkillNode> actionSkills;
        List<SkillNode> passiveSkills;
        List<SkillNode> upgradeSkills;
        
        //Major Upgrades Unlocked//
        bool doubleJumpUnlocked;
        bool wallJumpUnlocked;
        bool rockBreakerUnlocked;
        bool slideUnlocked;
        bool swimUnlocked;
        bool keyUnlocked;
        
        //This constructor is to be used to initialize a character from a loaded game//
        //I know it's a bit messy, but there's a lot of data to initialize!//
        public CharacterEntry(PlayerRace playerRace, PlayerClass playerClass, string name, int level, int exp, int playerHP, int playerMP, int playerSTR, int playerINT, int playerEND, int playerSPR, int playerAGI, 
                              int playerLUCK, int weapon, int armor, int accessory1, int accessory2, int rune1, int rune2, int[] skills, int skill1, int skill2, bool doubleJumpUnlocked, bool wallJumpUnlocked,
                              bool rockBreakerUnlocked, bool slideUnlocked, bool swimUnlocked, bool keyUnlocked)
        {
            this.playerRace = playerRace;
            this.playerClass = playerClass;
            this.name = name;
            this.level = level;
            this.exp = exp;
            this.playerHP = playerHP;
            this.playerMP = playerMP;
            this.playerSTR = playerSTR;
            this.playerINT = playerINT;
            this.playerEND = playerEND;
            this.playerSPR = playerSPR;
            this.playerAGI = playerAGI;
            this.playerLUCK = playerLUCK;
            
            this.skillSTR = 0;
            this.skillINT = 0;
            this.skillEND = 0;
            this.skillSPR = 0;
            this.skillAGI = 0;
            this.skillLUCK = 0;
            
            InitEquipment(weapon, armor, accessory1, accessory2, rune1, rune2);
            
            actionSkills = new List<SkillNode>();
            passiveSkills = new List<SkillNode>();
            upgradeSkills = new List<SkillNode>();
            InitSkills(skills);
            
            EquipSkill(skill1, 0);
            EquipSkill(skill2, 0);
            
            this.doubleJumpUnlocked = doubleJumpUnlocked;
            this.wallJumpUnlocked = wallJumpUnlocked;
            this.rockBreakerUnlocked = rockBreakerUnlocked;
            this.slideUnlocked = slideUnlocked;
            this.swimUnlocked = swimUnlocked;
            this.keyUnlocked = keyUnlocked;
        }
        
        //This constructor is used to create a new character//
        public CharacterEntry(PlayerRace playerRace, PlayerClass playerClass, string name)
        {
            this.playerRace = playerRace;
            this.playerClass = playerClass;
            this.name = name;
            this.level = 1;
            this.exp = 0;
            this.playerHP = playerClass.baseHP;
            this.playerMP = playerClass.baseMP;
            this.playerSTR = playerRace.baseSTR;
            this.playerINT = playerRace.baseINT;
            this.playerEND = playerRace.baseEND;
            this.playerSPR = playerRace.baseSPR;
            this.playerAGI = playerRace.baseAGI;
            this.playerLUCK = playerRace.baseLUCK;
            
            this.skillSTR = 0;
            this.skillINT = 0;
            this.skillEND = 0;
            this.skillSPR = 0;
            this.skillAGI = 0;
            this.skillLUCK = 0;
            
            InitEquipment();
            
            actionSkills = new List<SkillNode>();
            passiveSkills = new List<SkillNode>();
            upgradeSkills = new List<SkillNode>();
            
            skill1 = SkillData.GetEmptyNode();
            skill2 = SkillData.GetEmptyNode();
            
            this.doubleJumpUnlocked = false;
            this.wallJumpUnlocked = false;
            this.rockBreakerUnlocked = false;
            this.slideUnlocked = false;
            this.swimUnlocked = false;
            this.keyUnlocked = false;
        }
        
        //Get Stats//
        public int GetHP(bool getTotal = false)
        {
            if(!getTotal)
                return playerHP;
            
            else
            {
                int natural = playerHP;
                int bonus = (int)(GetEND(true) * ENDtoHP);
                
                return natural + bonus;
            }
        }
        
        public int GetMP(bool getTotal = false)
        {
            if(!getTotal)
                return playerMP;
            
            else
            {
                int natural = playerMP;
                int bonus = (int)(GetSPR(true) * SPRtoMP);
                
                return natural + bonus;
            }
        }
        
        public int GetSTR(bool getTotal = false)
        {
            if(!getTotal)
                return playerSTR;
            
            else
            {
                int natural = playerSTR;
                int bonus = ItemData.GetArmor(armor).bonusSTR +
                            ItemData.GetWeapon(weapon).bonusSTR +
                            ItemData.GetAccessory(accessory1).bonusSTR +
                            ItemData.GetAccessory(accessory2).bonusSTR +
                            skillSTR;
                
                return natural + bonus;
            }
        }
        
        public int GetINT(bool getTotal = false)
        {
            if(!getTotal)
                return playerINT;
            
            else
            {
                int natural = playerINT;
                int bonus = ItemData.GetArmor(armor).bonusINT +
                            ItemData.GetWeapon(weapon).bonusINT +
                            ItemData.GetAccessory(accessory1).bonusINT +
                            ItemData.GetAccessory(accessory2).bonusINT +
                            skillINT;
                
                return natural + bonus;
            }
        }
        
        public int GetEND(bool getTotal = false)
        {
            if(!getTotal)
                return playerEND;
            
            else
            {
                int natural = playerEND;
                int bonus = ItemData.GetArmor(armor).bonusEND +
                            ItemData.GetWeapon(weapon).bonusEND +
                            ItemData.GetAccessory(accessory1).bonusEND +
                            ItemData.GetAccessory(accessory2).bonusEND +
                            skillEND;
                
                return natural + bonus;
            }
        }
        
        public int GetSPR(bool getTotal = false)
        {
            if(!getTotal)
                return playerSPR;
            
            else
            {
                int natural = playerSPR;
                int bonus = ItemData.GetArmor(armor).bonusSPR +
                            ItemData.GetWeapon(weapon).bonusSPR +
                            ItemData.GetAccessory(accessory1).bonusSPR +
                            ItemData.GetAccessory(accessory2).bonusSPR +
                            skillSPR;
                
                return natural + bonus;
            }
        }
        
        public int GetAGI(bool getTotal = false)
        {
            if(!getTotal)
                return playerAGI;
            
            else
            {
                int natural = playerAGI;
                int bonus = ItemData.GetArmor(armor).bonusAGI +
                            ItemData.GetWeapon(weapon).bonusAGI +
                            ItemData.GetAccessory(accessory1).bonusAGI +
                            ItemData.GetAccessory(accessory2).bonusAGI +
                            skillAGI;
                
                return natural + bonus;
            }
        }
        
        public int GetLUCK(bool getTotal = false)
        {
            if(!getTotal)
                return playerLUCK;
            
            else
            {
                int natural = playerLUCK;
                int bonus = ItemData.GetArmor(armor).bonusLUCK +
                            ItemData.GetWeapon(weapon).bonusLUCK +
                            ItemData.GetAccessory(accessory1).bonusLUCK +
                            ItemData.GetAccessory(accessory2).bonusLUCK +
                            skillLUCK;
                
                return natural + bonus;
            }
        }
        
        /////////////////////////////////////////////////////////////////////////////
        //int GetDEF()                                                             //
        //Calculates and returns effective defense based on Endurance and Equipment//
        /////////////////////////////////////////////////////////////////////////////
        public int GetDEF()
        {
            return (int)(GetEND(true) * ENDtoDEF);
        }
        
        ////////////////////////////////////////////////////////////////////////////////
        //int GetMDEF()                                                               //
        //Calculates and returns effective magic defense based on Spirit and Equipment//
        ////////////////////////////////////////////////////////////////////////////////
        public int GetMDEF()
        {
            return (int)(GetSPR(true) * SPRtoMDEF);
        }
        
        ///////////////////////////////////////////////////////////////////////////
        //int GetATK()                                                           //
        //Calculates and returns effective attack based on Strength and Equipment//
        ///////////////////////////////////////////////////////////////////////////
        public int GetATK()
        {
            int natural = (int)(GetSTR(true) * STRtoATK);
            
            return natural + ItemData.GetWeapon(weapon).attack;
        }
        
        //////////////////////////////////////////////////////////////////////////////////
        //int GetMATK()                                                                 //
        //Calculates and returns effective magic attack based on Intellect and Equipment//
        //////////////////////////////////////////////////////////////////////////////////
        public int GetMATK()
        {
            int natural = (int)(GetINT(true) * INTtoMATK);
            
            return natural + ItemData.GetWeapon(weapon).mattack;
        }
        
        public SkillNode GetSkill1()
        {
            return skill1;
        }
        
        public SkillNode GetSkill2()
        {
            return skill2;
        }
        
        public bool HasDoubleJump()
        {
            return doubleJumpUnlocked;
        }
        
        public bool HasWallJump()
        {
            return wallJumpUnlocked;
        }
        
        public bool HasRockBreaker()
        {
            return rockBreakerUnlocked;
        }
        
        public bool HasSlide()
        {
            return slideUnlocked;
        }
        
        public bool HasSwim()
        {
            return swimUnlocked;
        }
        
        public bool HasKey()
        {
            return keyUnlocked;
        }
        
        //bool AddSkill(SkillNode)
        //Adds the specified skill to the character
        public bool AddSkill(SkillNode node)
        {
            if(node.GetSkillType() == SkillData.SkillType.Attack)
            {
                if(!actionSkills.Contains(node))
                {
                    actionSkills.Add(node);
                    return true;
                }
                
                return false;
            }
            
            else if(node.GetSkillType() == SkillData.SkillType.StatPassive)
            {
                if(!passiveSkills.Contains(node))
                {
                    passiveSkills.Add(node);
                    return true;
                }
                
                return false;
            }
            
            else if(node.GetSkillType() == SkillData.SkillType.SkillPassive)
            {
                if(!upgradeSkills.Contains(node))
                {
                    upgradeSkills.Add(node);
                    return true;
                }
                
                return false;
            }
            
            //I don't work with Enums too often.                                     //
            //I can't imagine anything will get past the if-else, but just in case...//
            return false;
        }
        
        //////////////////////////////////////////////////////////
        //void CalculateBonuses()                               //
        //Calculates any bonus stats from learned passive skills//
        //Called once before loading each stage                 //
        //////////////////////////////////////////////////////////
        public void CalculateBonuses()
        {
            for(int i = 0; i < passiveSkills.Count; i++)
            {
                PassiveSkill temp = (PassiveSkill)passiveSkills[i].GetSkill();
                skillSTR += temp.bonusSTR;
                skillINT += temp.bonusINT;
                skillEND += temp.bonusEND;
                skillSPR += temp.bonusSPR;
                skillAGI += temp.bonusAGI;
                skillLUCK += temp.bonusLUCK;
            }
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        //bool EquipSkill(int, int)                                                                        //
        //Equips the skill specified by id to the specified skill slot                                     //
        //Returns false if the character does not know the skill or the skill is equipped in the other slot//
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool EquipSkill(int id, int slot)
        {
            if(!actionSkills.Contains(SkillData.GetNode(id)))
            {
                return false;
            }
            
            if(slot == 0)
            {
                if(skill2 != null && skill2.GetID() == id)
                    return false;
                
                skill1 = SkillData.GetNode(id);
            }
            
            else if(slot == 1)
            {
                if(skill1 != null && skill1.GetID() == id)
                    return false;
                
                skill2 = SkillData.GetNode(id);
            }
            
            return true;
        }
        
        public void UnlockDoubleJump()
        {
            doubleJumpUnlocked = true;
        }
        
        public void UnlockWallJump()
        {
            wallJumpUnlocked = true;
        }
        
        public void UnlockRockBreaker()
        {
            rockBreakerUnlocked = true;
        }
        
        public void UnlockSlide()
        {
            slideUnlocked = true;
        }
        
        public void UnlockSwim()
        {
            swimUnlocked = true;
        }
        
        public void UnlockKey()
        {
            keyUnlocked = true;
        }
        
        //////////////////////////////////////////////////////////
        //void InitEquipment(int, int, int, int, int, int)      //
        //Initializes the character's equipment to the input IDs//
        //////////////////////////////////////////////////////////
        private void InitEquipment(int weapon = 0, int armor = 0, int accessory1 = 0, int accessory2 = 0, int rune1 = 0, int rune2 = 0)
        {
            this.weapon = weapon;
            this.armor = armor;
            this.accessory1 = accessory1;
            this.accessory2 = accessory2;
            this.rune1 = rune1;
            this.rune2 = rune2;
        }
        
        //////////////////////////////////////
        //void InitSkills(int[])            //
        //Initializes the character's skills//
        //////////////////////////////////////
        private void InitSkills(int[] ids)
        {
            for(int i = 0; i < ids.Length; i++)
            {
                AddSkill(SkillData.GetNode(ids[i]));
            }
        }
    }
}
