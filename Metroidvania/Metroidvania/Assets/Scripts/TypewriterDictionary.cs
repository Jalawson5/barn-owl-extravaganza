using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TypewriterDictionary keeps track of variables used in text//
public class TypewriterDictionary
{
    Dictionary<string, string> vars = new Dictionary<string, string>();
    
    ////////////////////////////////////////
    //void InitDictionary()               //
    //Initializes all dictionary variables//
    ////////////////////////////////////////
    public void InitDictionary()
    {   
        ResetDictionary();
        LoadPlayerVariables();
    }
    
    private void ResetDictionary()
    {
        vars.Clear();
    }
    
    ////////////////////////////////////////
    //void LoadPlayerVariables()          //
    //Initializes player related variables//
    ////////////////////////////////////////
    private void LoadPlayerVariables()
    {
        vars.Add("_NAME", CharacterData.currentChar.GetName());
        vars.Add("_RACE", CharacterData.currentChar.GetRace().name);
        vars.Add("_CLASS" , CharacterData.currentChar.GetClass().name);
        
        if(CharacterData.currentChar.GetGender())
        {
            vars.Add("_SUBONOUN", "she");
            vars.Add("_OBPRONOUN", "her");
            vars.Add("_POSPRONOUN", "her");
            vars.Add("_POSSPRONOUN", "hers");
            vars.Add("_REFPRONOUN", "herself");
        }
        
        else
        {
            vars.Add("_SUBONOUN", "he");
            vars.Add("_OBPRONOUN", "him");
            vars.Add("_POSPRONOUN", "his");
            vars.Add("_POSSPRONOUN", "his");
            vars.Add("_REFPRONOUN", "himself");
        }
    }
}
