using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private int choices; //Number of choices//
    private int index; //Starting at 0//
    private float lineDist; //Distance between lines//
    private Vector2 startPosition; //Starting position of the cursor//
    
    private bool isInitialized;
    
    void Start()
    {
        index = 0;
        choices = 2;
        startPosition = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(isInitialized)
        {
            if(Input.GetKeyDown("down"))
            {
                index++;
                if(index >= choices)
                {
                    index = 0;
                }
            }
            
            else if(Input.GetKeyDown("up"))
            {
                index--;
                
                if(index < 0)
                {
                    index = choices - 1;
                }
            }
            
            transform.position = new Vector3(transform.position.x, startPosition.y - lineDist * index, 0);
        }
    }
    
    public void InitValues(int choices, float lineDist, Vector2 startPosition)
    {
        this.choices = choices;
        this.lineDist = lineDist;
        this.startPosition = startPosition;
        
        isInitialized = true;
    }
    
    public int GetChoice()
    {
        return this.index;
    }
}
