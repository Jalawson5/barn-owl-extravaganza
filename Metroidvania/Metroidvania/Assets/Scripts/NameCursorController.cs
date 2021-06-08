using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameCursorController : MonoBehaviour
{
    [SerializeField]
    private Transform[] flatPositions;
    
    private Transform[,] positions;
    
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
    
        BuildPositions();
        
        cursorX = 0;
        cursorY = 0;
        
        maxNameSize = 12;
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
        
        transform.position = positions[cursorX, cursorY].position;
        Debug.Log("coord: " + cursorX + ", " + cursorY);
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
}
