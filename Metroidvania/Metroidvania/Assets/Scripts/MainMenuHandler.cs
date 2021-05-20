using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void OnNewGamePressed()
    {
        SceneManager.LoadScene("CharacterCreation");
    }
    
    public void OnLoadGamePressed()
    {
        SceneManager.LoadScene("LoadData");
    }
    
    public void OnOptionsPressed()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    
    public void OnExitPressed()
    {
        Application.Quit();
    }
}
