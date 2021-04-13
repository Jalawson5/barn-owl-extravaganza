using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree
{
    private int id;
    private PlayerClass playerClass;
    private string treeName;
    private Dictionary<int, bool> skills;
    
    public SkillTree(int id, PlayerClass playerClass, string treeName)
    {
        this.id = id;
        this.playerClass = playerClass;
        this.treeName = treeName;
        this.skills = new Dictionary<int, bool>();
    }
    
    //Getters//
    public int GetID()
    {
        return id;
    }
    
    public PlayerClass GetClass()
    {
        return playerClass;
    }
    
    public string GetName()
    {
        return treeName;
    }
    
    ///////////////////////////////////////////
    //bool AddSkill(int)                     //
    //Used by SkillData to construct the tree//
    ///////////////////////////////////////////
    public bool AddSkill(int id)
    {
        if(skills.ContainsKey(id))
            return false;
        
        skills.Add(id, false);
        return true;
    }
    
    ///////////////////////////////////////////
    //bool AddSkills(int[])                  //
    //Used by SkillData to construct the tree//
    ///////////////////////////////////////////
    public bool AddSkills(int[] ids)
    {
        for(int i = 0; i < ids.Length; i++)
        {
            AddSkill(ids[i]);
        }
        
        return true;
    }
    
    //////////////////////////////////////////////////////////////////
    //bool IsUnlocked(int)                                          //
    //Returns if the specified skill is unlocked                    //
    //returns false if the tree does not contain the specified skill//
    //////////////////////////////////////////////////////////////////
    public bool IsUnlocked(int id)
    {
        if(skills.ContainsKey(id))
            return skills[id];
        
        return false;
    }
    
    //////////////////////////////////////////////////////////////////////////
    //bool Unlock(int)                                                      //
    //Unlocks the specified skill                                           //
    //Returns true if the skill is unlocked, false if the skill is not found//
    //////////////////////////////////////////////////////////////////////////
    public bool Unlock(int id)
    {
        if(skills.ContainsKey(id))
        {
            skills[id] = true;
            return true;
        }
        
        return false;
    }
}
