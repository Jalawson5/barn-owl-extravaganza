using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterController : MonoBehaviour
{
    public static TypewriterController instance;
    
    [SerializeField]
    private Sprite[] sprites;

    private static float baseDist;
    
    private Dictionary<char, Letter> letters;
    
    private Vector2 startPosition;
    private float lineWidth; //How long can each line of text be?//
    private float lineDist; //How much space between each line of text?//
    private float xPointer; //Current x position of the pointer//
    private float yPointer; //Current y position of the pointer//
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        InitCharacters();
    }

    void Update()
    {
        
    }
    
    private class Letter
    {
        public Sprite sprite; //Sprite for this character//
        public int width; //Width of the sprite in pixels//
        
        //////////////////////////////////////////////////////////////////////////////////////////////////
        //The typewriter should leave space after each character depending on the width of the character//
        //For the test font, this conversion to Unity units is as follows:                              //
        //                                                                                              //
        //1px = 0.15                                                                                    //
        //3px = 0.25                                                                                    //
        //4px = 0.30                                                                                    //
        //5px = 0.35                                                                                    //
        //6px = 0.40                                                                                    //
        //7px = 0.45                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////
        
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
    
    //////////////////////////////////////////////////////////////////////////////
    //void InitCharacters()                                                     //
    //Meticulously initializes each individual character used by the typewritter//
    //////////////////////////////////////////////////////////////////////////////
    private void InitCharacters()
    {
        int i = 0; //Index of sprite array, makes things easier for me in the long-run//
        
        //Uppercase Letters//
        letters.Add('A', new Letter(sprites[i++], 6));
        letters.Add('B', new Letter(sprites[i++], 6));
        letters.Add('C', new Letter(sprites[i++], 6));
        letters.Add('D', new Letter(sprites[i++], 6));
        letters.Add('E', new Letter(sprites[i++], 6));
        letters.Add('F', new Letter(sprites[i++], 6));
        letters.Add('G', new Letter(sprites[i++], 6));
        letters.Add('H', new Letter(sprites[i++], 6));
        letters.Add('I', new Letter(sprites[i++], 5));
        letters.Add('J', new Letter(sprites[i++], 5));
        letters.Add('K', new Letter(sprites[i++], 5));
        letters.Add('L', new Letter(sprites[i++], 5));
        letters.Add('M', new Letter(sprites[i++], 7));
        letters.Add('N', new Letter(sprites[i++], 6));
        letters.Add('O', new Letter(sprites[i++], 6));
        letters.Add('P', new Letter(sprites[i++], 6));
        letters.Add('Q', new Letter(sprites[i++], 6));
        letters.Add('R', new Letter(sprites[i++], 6));
        letters.Add('S', new Letter(sprites[i++], 5));
        letters.Add('T', new Letter(sprites[i++], 5));
        letters.Add('U', new Letter(sprites[i++], 6));
        letters.Add('V', new Letter(sprites[i++], 5));
        letters.Add('W', new Letter(sprites[i++], 7));
        letters.Add('X', new Letter(sprites[i++], 7));
        letters.Add('Y', new Letter(sprites[i++], 5));
        letters.Add('Z', new Letter(sprites[i++], 6));
        
        //Lowercase Letters//
        letters.Add('a', new Letter(sprites[i++], 5));
        letters.Add('b', new Letter(sprites[i++], 4));
        letters.Add('c', new Letter(sprites[i++], 4));
        letters.Add('d', new Letter(sprites[i++], 4));
        letters.Add('e', new Letter(sprites[i++], 4));
        letters.Add('f', new Letter(sprites[i++], 4));
        letters.Add('g', new Letter(sprites[i++], 4));
        letters.Add('h', new Letter(sprites[i++], 4));
        letters.Add('i', new Letter(sprites[i++], 1));
        letters.Add('j', new Letter(sprites[i++], 4));
        letters.Add('k', new Letter(sprites[i++], 4));
        letters.Add('l', new Letter(sprites[i++], 1));
        letters.Add('m', new Letter(sprites[i++], 5));
        letters.Add('n', new Letter(sprites[i++], 5));
        letters.Add('o', new Letter(sprites[i++], 4));
        letters.Add('p', new Letter(sprites[i++], 4));
        letters.Add('q', new Letter(sprites[i++], 4));
        letters.Add('r', new Letter(sprites[i++], 5));
        letters.Add('s', new Letter(sprites[i++], 4));
        letters.Add('t', new Letter(sprites[i++], 3));
        letters.Add('u', new Letter(sprites[i++], 4));
        letters.Add('v', new Letter(sprites[i++], 5));
        letters.Add('w', new Letter(sprites[i++], 5));
        letters.Add('x', new Letter(sprites[i++], 4));
        letters.Add('y', new Letter(sprites[i++], 4));
        letters.Add('z', new Letter(sprites[i++], 4));
        
        //Numbers//
        letters.Add('1', new Letter(sprites[i++], 3));
        letters.Add('2', new Letter(sprites[i++], 5));
        letters.Add('3', new Letter(sprites[i++], 5));
        letters.Add('4', new Letter(sprites[i++], 5));
        letters.Add('5', new Letter(sprites[i++], 5));
        letters.Add('6', new Letter(sprites[i++], 5));
        letters.Add('7', new Letter(sprites[i++], 5));
        letters.Add('8', new Letter(sprites[i++], 5));
        letters.Add('9', new Letter(sprites[i++], 5));
        letters.Add('0', new Letter(sprites[i++], 5));
        
        //Punctuation//
        letters.Add('.', new Letter(sprites[i++], 1));
        letters.Add(',', new Letter(sprites[i++], 1));
        letters.Add('?', new Letter(sprites[i++], 5));
        letters.Add('!', new Letter(sprites[i++], 1));
        letters.Add(':', new Letter(sprites[i++], 1));
        letters.Add(';', new Letter(sprites[i++], 1));
        letters.Add('\'', new Letter(sprites[i++], 1));
        letters.Add('"', new Letter(sprites[i++], 3));
    }
}
