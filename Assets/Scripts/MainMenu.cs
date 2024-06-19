using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartAdventure()
    {
        SceneManager.LoadScene("Grid");
    }
    
    public void BackToMM()
    {
        SceneManager.LoadScene("Menus");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
