using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData
{
    public enum SkillType{Attack, StatPassive, SkillPassive} //Attack is a usable skill, StatPassive is a stat boost, SkillPassive is an upgrade to a skill//
    public enum TargetType{Self, Enemy, Ally} //Self only affects the caster, Enemy affects any enemies within the hitbox, Ally affects any allies within the hitbox//
    
    private static Skill[] skills;
    private static SkillTree[] trees;
    private static SkillNode[] nodes;
    
    private static SkillNode blankNode; //To avoid null reference exceptions, create an empty "default" node//
    
    //////////////////////////////////////////////////////////////////////////
    //void InitSkills()                                                     //
    //Retrieves skills from the Resources folder and stores them in an array//
    //////////////////////////////////////////////////////////////////////////
    public static void InitSkills()
    {
        //Initialize Skills array//
        skills = (Skill[])Resources.LoadAll<Skill>("Skills");
        
        //Sort by ID for easy access in the future//
        Array.Sort(skills, delegate(Skill x, Skill y){return (x.id < y.id)?1:0;});
        
        //Initialize Skill Trees//
        InitTrees();
        
        blankNode = new SkillNode(-1, null, 0, null);
    }
    
    private static void InitTrees()
    {
        //Create all possible skill trees//
        trees = new SkillTree[CharacterData.NumClasses * 3];
        nodes = new SkillNode[skills.Length];
        
        //Warrior Skill Trees//
        //Skill Tree names not final//
        trees[0] = new SkillTree(0, CharacterData.warrior, "Knight");
        //Add Skills//
        
        //Barbarian is my scapegoat for testing//
        trees[1] = new SkillTree(1, CharacterData.warrior, "Barbarian");
        nodes[0] = new SkillNode(1, null, 1, skills[0]);
        nodes[1] = new SkillNode(2, new int[]{0}, 5, skills[1]);
        nodes[2] = new SkillNode(3, new int[]{1}, 10, skills[2]);
        
        trees[1].AddSkills(new int[]{0, 1, 2});
        
        trees[2] = new SkillTree(2, CharacterData.warrior, "Spellsword");
        //Add Skills//
        
        
        //Wizard Skill Trees//
        trees[3] = new SkillTree(3, CharacterData.wizard, "Battlemage");
        //Add Skills//
        
        trees[4] = new SkillTree(4, CharacterData.wizard, "Archmage");
        //Add Skills//
        
        trees[5] = new SkillTree(5, CharacterData.wizard, "Sorcerer");
        //Add Skills//
        
        
        //Thief Skill Trees//
        trees[6] = new SkillTree(6, CharacterData.thief, "Assassin");
        //Add Skills//
        
        trees[7] = new SkillTree(7, CharacterData.thief, "Bandit");
        //Add Skills//
        
        trees[8] = new SkillTree(8, CharacterData.thief, "Ninja");
        //Add Skills//
    }
    
    public static Skill GetSkill(int id)
    {
        return skills[id];
    }
    
    public static SkillTree GetTree(int id)
    {
        return trees[id];
    }
    
    public static SkillNode GetNode(int id)
    {
        return nodes[id];
    }
    
    public static SkillNode GetEmptyNode()
    {
        return blankNode;
    }
}
