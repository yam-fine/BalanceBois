using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGameFor2()
    {
        // the next scene
        // should add if it is 2 players or 4 players .
        SceneManager.LoadScene("Map1");
    }

    public void PlayGameFor4()
    {
        // the next scene
        // should add if it is 2 players or 4 players .
        SceneManager.LoadScene("Map2");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
