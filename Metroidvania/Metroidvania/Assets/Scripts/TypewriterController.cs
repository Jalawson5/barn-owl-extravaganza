using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterController : MonoBehaviour
{
    private static float baseDist;
    private Dictionary<char, Letter> letters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private class Letter
    {
        public Sprite sprite; //Sprite for this character//
        public int width; //Width of the sprite in pixels//
        
        public Letter(Sprite sprite, int width)
        {
            this.sprite = sprite;
            this.width = width;
        }
        
        ////////////////////////////////////////////////////
        //float DistToNext()                              //
        //returns the Unity distance to the next character//
        //used to increment the dialogue pointer          //
        ////////////////////////////////////////////////////
        public float DistToNext()
        {
            return TypewriterController.baseDist * (width - 1);
        }
    }
}
