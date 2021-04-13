using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNode
{
    private int treeId;
    private bool unlocked;
    private SkillData.SkillType type;
    private int[] reqs; //requirements as an array of Skill id's//
    private int levelReq; //Required player level to unlock skill//
    private Skill skill;
    
    public SkillNode(int treeId, int[] reqs, int levelReq, Skill skill)
    {
        this.treeId = treeId;
        this.reqs = reqs;
        this.levelReq = levelReq;
        this.skill = skill;
        
        this.unlocked = false;
        
        if(skill != null)
            this.type = skill.type;
        
        else
            this.type = SkillData.SkillType.StatPassive;
    }
    
    //Getters//    
    public int GetTree()
    {
        return treeId;
    }
    
    public bool IsUnlocked()
    {
        return unlocked;
    }
    
    public bool CanUnlock(int level)
    {
        if(reqs == null)
            return true;
        
        else if(level < levelReq)
            return false;
        
        for(int i = 0; i < reqs.Length; i++)
        {
            if(!SkillData.GetTree(treeId).IsUnlocked(reqs[i]))
            {
                return false;
            }
        }
        
        return true;
    }
    
    public SkillData.SkillType GetSkillType()
    {
        return type;
    }
    
    public int GetID()
    {
        return skill.id;
    }
    
    public string GetName()
    {
        return skill.name;
    }
    
    public bool Unlock(int level)
    {
        if(!CanUnlock(level))
            return false;
        
        unlocked = true;
        
        return true;
    }
    
    public Skill GetSkill()
    {
        return skill;
    }
}
