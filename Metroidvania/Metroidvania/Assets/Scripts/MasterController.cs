using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    public static MasterController instance;
    public bool isPaused;
    
    public ControllerSettings controls;
    
    //AttackData prefabs//
    
    
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

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        instance.controls = new ControllerSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(instance.controls.GetPauseKey()))
        {
            if(!isPaused)
            {
                instance.PauseGame();
            }
            
            else
            {
                instance.UnpauseGame();
            }
        }
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        
        //Open pause menu//
    }
    
    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        
        //Close pause menu//
    }
}
