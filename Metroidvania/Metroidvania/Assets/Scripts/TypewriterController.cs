using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterController : MonoBehaviour
{
    public static TypewriterController instance;
    
    [SerializeField]
    private GameObject letterPrefab;
    
    [SerializeField]
    private GameObject textboxPrefab;
    
    [SerializeField]
    private GameObject choiceboxPrefab;
    
    [SerializeField]
    private GameObject choiceCursorPrefab;
    
    private GameObject choiceCursor;
    
    private GameObject tempTextbox;
    private GameObject tempChoicebox;
    
    private DialogueEntry currentEntry;
    
    [SerializeField]
    private Sprite[] sprites;

    private static float baseDist;
    private static float distInc;
    
    private Dictionary<char, Letter> letters;
    
    private List<GameObject> typedText;
    
    [SerializeField]
    private Vector2 startPosition; //Starting position for dialogue box text//
    private Vector2 choicePosition; //Starting position for choice box text//
    private float lineWidth; //Maximum x position for dialogue//
    private float lineDist; //How much space between each line of text?//
    private float maxHeight; //Maximum number of lines//
    private float numLine; //Number of the current line//
    private float xPointer; //Current x position of the pointer//
    private float yPointer; //Current y position of the pointer//
    private float spaceWidth; //Width of the gap between words//
    
    private string dialogue; //List of dialogue to type//
    private string[] words; //List of words in current dialogue//
    private int wordIndex; //Index of the current word//
    private string current; //Current word to type//
    private int currentIndex; //Index within the current word//
    private int dialogueIndex; //Current dialogue//
    private bool isTyping; //Is the Typewriter currently typing?//
    private bool waitForInput; //Is the Typewriter waiting for input?//
    private float typeSpeed; //Time interval to type each character//
    private float typeTimer; //Timer for typing each character//
    
    public MasterController master;
    
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
        
        baseDist = 0.1f;
        distInc = 0.05f;
        
        typedText = new List<GameObject>();
        
        startPosition = new Vector2(-4.5f, -2f);
        choicePosition = new Vector2(3.25f, -0.25f);
        
        xPointer = startPosition.x;
        yPointer = startPosition.y;
        lineWidth = 4.5f;
        lineDist = 0.35f;
        maxHeight = 7;
        numLine = 1;
        spaceWidth = 0.2f;
        
        wordIndex = 0;
        currentIndex = 0;
        dialogueIndex = 0;
        isTyping = false;
        waitForInput = false;
        
        typeSpeed = 0.05f;
        typeTimer = 0f;
        
        master = MasterController.instance;
    }

    void Update()
    {
        if(isTyping)
        {
            if(typeTimer < typeSpeed)
            {
                typeTimer += Time.deltaTime;
            }
            
            else
            {
                Debug.Log(current);
                Debug.Log(current[currentIndex]);
                GameObject temp = Instantiate(letterPrefab, new Vector3(xPointer, yPointer, 0), Quaternion.identity);
                Letter tempLetter = letters[current[currentIndex]];
                temp.GetComponent<SpriteRenderer>().sprite = tempLetter.sprite;
                typedText.Add(temp);
                xPointer += tempLetter.DistToNext();
                currentIndex++;
            
                //If current word has ended, move to next word//
                if(currentIndex >= current.Length)
                {
                    wordIndex++;
                    currentIndex = 0;
                    xPointer += spaceWidth;
                    
                    if((xPointer + WordWidth(current)) > lineWidth)
                    {
                        yPointer -= lineDist;
                        numLine++;
                        xPointer = startPosition.x;
                    }
                }
            
                //If all words in the dialogue are written, or dialogue box is full, wait for input for next dialogue//
                if(numLine > maxHeight || wordIndex >= words.Length)
                {
                    isTyping = false;
                    currentIndex = 0;
                    
                    waitForInput = true;
                    
                    if(currentEntry.hasChoice && wordIndex >= words.Length)
                    {
                        tempChoicebox = Instantiate(choiceboxPrefab);
                        TypeChoices();
                        CreateCursor();
                    }
                }
            
                else
                {
                    current = words[wordIndex];
                }
                
                typeTimer = 0f;
            }
        }
        
        else if(waitForInput && Input.GetKeyDown(master.controls.GetAttackKey()))
        {
            waitForInput = false;
            xPointer = startPosition.x;
            yPointer = startPosition.y;
            EraseText();
            
            if(wordIndex < words.Length)
            {
                current = words[wordIndex];
                isTyping = true;
                numLine = 0;
            }
            
            else if(choiceCursor != null)
            {
                if(choiceCursor.GetComponent<CursorController>().GetChoice() == 0)
                {
                    TypeDialogue(currentEntry.result1);
                }
                
                else
                {
                    TypeDialogue(currentEntry.result2);
                }
                
                Destroy(choiceCursor);
                Destroy(tempChoicebox);
                choiceCursor = null;
            }
            
            /*else if(currentEntry.hasChoice)
            {
                tempChoicebox = Instantiate(choiceboxPrefab);
                TypeChoices();
                CreateCursor();
            }*/
            
            else
            {
                MasterController.instance.isPaused = false;
                
                Destroy(tempTextbox);
            }
        }
    }
    
    public bool IsTyping()
    {
        return isTyping;
    }
    
    public void TypeDialogue(DialogueEntry dialogue)
    {
        if(!isTyping && !waitForInput)
        {
            this.dialogue = dialogue.text;
            isTyping = true;
            wordIndex = 0;
            currentIndex = 0;
            
            words = this.dialogue.Split(' ');
            current = words[0];
            
            MasterController.instance.isPaused = true;
            
            if(tempTextbox == null)
                tempTextbox = Instantiate(textboxPrefab);
            
            currentEntry = dialogue;
            Debug.Log(currentEntry.text);
        }
    }
    
    private void TypeChoices()
    {
        xPointer = choicePosition.x;
        yPointer = choicePosition.y;
        
        for(int i = 0; i < currentEntry.choice1.Length; i++)
        {
            if(currentEntry.choice1[i] == ' ')
            {
                xPointer += spaceWidth;
                continue;
            }
            
            GameObject temp = Instantiate(letterPrefab, new Vector3(xPointer, yPointer, 0), Quaternion.identity);
            Letter tempLetter = letters[currentEntry.choice1[i]];
            temp.GetComponent<SpriteRenderer>().sprite = tempLetter.sprite;
            typedText.Add(temp);
            
            xPointer += tempLetter.DistToNext();
        }
        
        xPointer = choicePosition.x;
        yPointer -= lineDist;
        
        for(int i = 0; i < currentEntry.choice2.Length; i++)
        {
            if(currentEntry.choice2[i] == ' ')
            {
                xPointer += spaceWidth;
                continue;
            }
            
            GameObject temp = Instantiate(letterPrefab, new Vector3(xPointer, yPointer, 0), Quaternion.identity);
            Letter tempLetter = letters[currentEntry.choice2[i]];
            temp.GetComponent<SpriteRenderer>().sprite = tempLetter.sprite;
            typedText.Add(temp);
            
            xPointer += tempLetter.DistToNext();
        }
        
        xPointer = startPosition.x;
        yPointer = startPosition.y;
    }
    
    private void CreateCursor()
    {
        choiceCursor = Instantiate(choiceCursorPrefab);
        choiceCursor.GetComponent<CursorController>().InitValues(2, lineDist, choicePosition - new Vector2(0.25f, 0));
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
            return baseDist + (distInc * width);
        }
    }
    
    //////////////////////////////////////////////////////////
    //float WordWidth(string)                               //
    //Calculates and returns the "width" of the input string//
    //////////////////////////////////////////////////////////
    private float WordWidth(string word)
    {
        float total = 0;
        for(int i = 0; i < word.Length; i++)
        {
            total += letters[word[i]].DistToNext();
        }
        
        return total;
    }
    
    private void EraseText()
    {
        for(int i = 0; i < typedText.Count; i++)
        {
            Destroy(typedText[i]);
        }
        
        typedText.Clear();
    }
    
    //////////////////////////////////////////////////////////////////////////////
    //void InitCharacters()                                                     //
    //Meticulously initializes each individual character used by the typewritter//
    //////////////////////////////////////////////////////////////////////////////
    private void InitCharacters()
    {
        int i = 0; //Index of sprite array, makes things easier for me in the long-run//
        
        letters = new Dictionary<char, Letter>();
        
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
