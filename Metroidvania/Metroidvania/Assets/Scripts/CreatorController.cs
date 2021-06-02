using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatorController : MonoBehaviour
{
    [SerializeField]
    private GameObject cursor;
    
    private float cursorStartY;
    
    [SerializeField]
    private Text raceText;
    
    [SerializeField]
    private Text genderText;

    [SerializeField]
    private Text classText;
    
    [SerializeField]
    private Text skinText;
    
    [SerializeField]
    private Text hairText;
    
    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text hpText;
    
    [SerializeField]
    private Text mpText;
    
    [SerializeField]
    private Text strText;
    
    [SerializeField]
    private Text intText;
    
    [SerializeField]
    private Text endText;
    
    [SerializeField]
    private Text sprText;
    
    [SerializeField]
    private Text agiText;
    
    [SerializeField]
    private Text luckText;
    
    private int raceIndex;
    private int genderIndex;
    private int classIndex;
    private int skinIndex;
    private int hairIndex;
    private string characterName;
    
    private int skinMax;
    private int hairMax;
    
    private string[] raceTexts;
    private string[] genderTexts;
    private string[] classTexts;
    
    private int index;
    private int max;
    private int direction;
    
    private bool isPaused;
    
    // Start is called before the first frame update
    void Start()
    {    
        cursorStartY = 150;
        
        raceIndex = 0;
        genderIndex = 0;
        classIndex = 0;
        skinIndex = 0;
        hairIndex = 0;
        characterName = "";
        
        skinMax = 6;
        hairMax = 10;
        
        raceTexts = new string[]{"Human", "Elf", "Dwarf", "Gnome", "Unnamed 1", "Unnamed 2", "Unnamed 3"};
        genderTexts = new string[]{"Male", "female"};
        classTexts = new string[]{"Warrior", "Wizard", "Thief"};
        
        index = 0;
        max = 6;
        direction = 0;
        
        UpdateStats();
        
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused)
        {
            if(Input.GetKeyDown("up"))
            {
                index--;
                if(index < 0)
                index = max;
            }
        
            else if(Input.GetKeyDown("down"))
            {
                index++;
                if(index > max)
                    index = 0;
            }
        
            cursor.transform.localPosition = new Vector3(cursor.transform.localPosition.x, cursorStartY - (50f * index), 0);
        
            if(index < 5)
            {
                if(Input.GetKeyDown("left"))
                {
                    direction = -1;
                }
            
                else if(Input.GetKeyDown("right"))
                {
                    direction = 1;
                }
            
                if(index == 0)
                {
                    raceIndex += direction;
                
                    if(raceIndex < 0)
                        raceIndex = raceTexts.Length - 1;
                
                    else if(raceIndex == raceTexts.Length)
                        raceIndex = 0;
                    
                    raceText.text = raceTexts[raceIndex];
                    UpdateStats();
                }
            
                else if(index == 1)
                {
                    genderIndex += direction;
                
                    if(genderIndex < 0)
                        genderIndex = 1;
                
                    else if(genderIndex > 1)
                        genderIndex = 0;
                    
                    genderText.text = genderTexts[genderIndex];
                }
            
                else if(index == 2)
                {
                    classIndex += direction;
                
                    if(classIndex < 0)
                        classIndex = classTexts.Length - 1;
                
                    else if(classIndex == classTexts.Length)
                        classIndex = 0;
                    
                    classText.text = classTexts[classIndex];
                    UpdateStats();
                }
            
                else if(index == 3)
                {
                    skinIndex += direction;
                
                    if(skinIndex < 0)
                        skinIndex = skinMax - 1;
                
                    else if(skinIndex == skinMax)
                        skinIndex = 0;
                
                    skinText.text = (skinIndex + 1).ToString();
                }
            
                else if(index == 4)
                {
                    hairIndex += direction;
                
                    if(hairIndex < 0)
                        hairIndex = hairMax - 1;
                
                    else if(hairIndex == hairMax)
                        hairIndex = 0;
                    
                    hairText.text = (hairIndex + 1).ToString();
                }
            }
            
            else if(index == 5 && Input.GetKeyDown(MasterController.instance.controls.GetSelectKey()))
            {
                
            }
        }
        
        direction = 0;
    }
    
    private void UpdateStats()
    {
        PlayerRace tempRace;
        PlayerClass tempClass;
        
        switch(raceTexts[raceIndex])
        {
            case "Human":
                tempRace = CharacterData.human;
                break;
            case "Elf":
                tempRace = CharacterData.elf;
                break;
            case "Dwarf":
                tempRace = CharacterData.dwarf;
                break;
            case "Gnome":
                tempRace = CharacterData.gnome;
                break;
            case "Unnamed 1":
                tempRace = CharacterData.unnamed1;
                break;
            case "Unnamed 2":
                tempRace = CharacterData.unnamed2;
                break;
            case "Unnamed 3":
                tempRace = CharacterData.unnamed3;
                break;
            default:
                Debug.Log("CreatorController: Invalid Race Index");
                return;
        }
        
        switch(classTexts[classIndex])
        {
            case "Warrior":
                tempClass = CharacterData.warrior;
                break;
            case "Wizard":
                tempClass = CharacterData.wizard;
                break;
            case "Thief":
                tempClass = CharacterData.thief;
                break;
            default:
                Debug.Log("CreatorController: Invalid Class Index");
                return;
        }
        Debug.Log(tempClass.name);
        hpText.text = (tempClass.baseHP).ToString();
        mpText.text = (tempClass.baseMP).ToString();
        strText.text = (tempRace.baseSTR + tempClass.growthSTR).ToString();
        intText.text = (tempRace.baseINT + tempClass.growthINT).ToString();
        endText.text = (tempRace.baseEND + tempClass.growthEND).ToString();
        sprText.text = (tempRace.baseSPR + tempClass.growthSPR).ToString();
        agiText.text = (tempRace.baseAGI + tempClass.growthAGI).ToString();
        luckText.text = (tempRace.baseLUCK + tempClass.growthLUCK).ToString();
    }
}
