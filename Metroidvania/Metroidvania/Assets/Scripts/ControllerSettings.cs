using System.Collections;
using System.Collections.Generic;

/////////////////////////////////////////////////////////////////////////////////
//This class contains the controls. For now, the controls are set to a default.//
//In the future, this class will allow for the player to change controls.      //
/////////////////////////////////////////////////////////////////////////////////
public class ControllerSettings
{
    private string pauseKey;
    private string leftKey;
    private string rightKey;
    private string upKey;
    private string downKey;
    
    private string jumpKey;
    private string attackKey;
    private string skill1Key;
    private string skill2Key;
    
    private string selectKey;
    private string cancelKey;
    
    //Default constructor sets controls to the keys I use for testing//
    public ControllerSettings()
    {
        pauseKey = "escape";
        
        jumpKey = "z";
        attackKey = "x";
        skill1Key = "a";
        skill2Key = "s";
        
        selectKey = jumpKey;
        cancelKey = attackKey;
    }
    
    //Getters//
    public string GetPauseKey()
    {
        return pauseKey;
    }
    
    public string GetJumpKey()
    {
        return jumpKey;
    }
    
    public string GetAttackKey()
    {
        return attackKey;
    }
    
    public string GetSkillOneKey()
    {
        return skill1Key;
    }
    
    public string GetSkillTwoKey()
    {
        return skill2Key;
    }
    
    public string GetSelectKey()
    {
        return selectKey;
    }
    
    public string GetCancelKey()
    {
        return cancelKey;
    }
}
