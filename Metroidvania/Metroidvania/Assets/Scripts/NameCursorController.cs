using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameCursorController : MonoBehaviour
{
    [SerializeField]
    private GameObject letterPrefab;

    [SerializeField]
    private Transform nameTextStart;
    
    private float typewriterX; //x position of the typewriter//
    
    private GameObject[] currentLetters; //Currently typed letters//
    private string currentName;
    
    private int letterIndex;

    [SerializeField]
    private Transform[] flatPositions;
    
    private Transform[,] positions;
    
    private float xOffset; //x position offset//
    private float yOffset; //y position offset//
    
    private int keyboardWidth;
    private int keyboardHeight;
    
    private int cursorX;
    private int cursorY;
    
    private int maxNameSize;
    
    // Start is called before the first frame update
    void Start()
    {
        keyboardWidth = 7;
        keyboardHeight = 7;
        
        xOffset = -4;
        yOffset = -4;
    
        BuildPositions();
        
        cursorX = 0;
        cursorY = 0;
        
        maxNameSize = 11;
        
        typewriterX = nameTextStart.position.x;
        currentLetters = new GameObject[maxNameSize];
        currentName = "";
        letterIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("right"))
        {
            cursorX++;
            if(cursorX > keyboardWidth)
            {
                cursorX = 0;
            }
        }
        
        else if(Input.GetKeyDown("left"))
        {
            cursorX--;
            if(cursorX < 0)
            {
                cursorX = keyboardWidth;
            }
        }
        
        else if(Input.GetKeyDown("up"))
        {
            cursorY--;
            if(cursorY < 0)
            {
                cursorY = keyboardHeight;
            }
        }
        
        else if(Input.GetKeyDown("down"))
        {
            cursorY++;
            if(cursorY > keyboardHeight)
            {
                cursorY = 0;
            }
        }
        
        transform.position = positions[cursorX, cursorY].position + new Vector3(xOffset, yOffset, 0);
        
        if(Input.GetKeyDown(MasterController.instance.controls.GetSelectKey()))
        {
            if(cursorX == keyboardWidth && cursorY == keyboardHeight)
            {
                CreatorController.instance.FinishName(currentName);
            }
            
            else if(cursorX == keyboardWidth - 1 && cursorY == keyboardHeight)
            {
                if(letterIndex > 0)
                {
                    BackLetter();
                }
            }
            
            else
            {
                if(letterIndex != maxNameSize)
                {
                    TypeLetter(positions[cursorX, cursorY].gameObject.name);
                }
            }
        }
    }
    
    private void BuildPositions()
    {
        positions = new Transform[keyboardWidth + 1, keyboardHeight + 1];
        int index = 0;
    
        for(int i = 0; i <= keyboardHeight; i++)
        {
            for(int k = 0; k <= keyboardWidth; k++, index++)
            {
                positions[k, i] = flatPositions[index];
                Debug.Log(positions[k, i].name);
            }
        }
    }
    
    private void TypeLetter(string letter)
    {
        char ind = letter[0];
        
        currentLetters[letterIndex] = Instantiate(letterPrefab, new Vector3(typewriterX, nameTextStart.position.y, 0), Quaternion.identity, transform.parent);
        TypewriterController.Letter temp = TypewriterController.instance.GetLetter(ind);
        currentLetters[letterIndex].GetComponent<Image>().sprite = temp.sprite;
        currentLetters[letterIndex].GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        typewriterX += temp.DistToNext() * 50;
        
        currentName += ind;
        letterIndex++;
    }
    
    private void BackLetter()
    {
        typewriterX = currentLetters[--letterIndex].transform.position.x;
        Destroy(currentLetters[letterIndex]);
        currentName = currentName.Remove(currentName.Length - 1);
    }
}
