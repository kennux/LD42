using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int gameSceneIndex;

    public void StartGame()
    {
        SceneManager.LoadScene(this.gameSceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
