using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static GameDifficulty difficulty = GameDifficulty.NULL;

    public int gameSceneIndex;

    public void StartGame(GameDifficulty difficulty)
    {
        MainMenu.difficulty = difficulty;
        SceneManager.LoadScene(this.gameSceneIndex);
    }

    public void StartEasyGame()
    {
        StartGame(GameDifficulty.EASY);
    }

    public void StartMediumGame()
    {
        StartGame(GameDifficulty.MEDIUM);
    }

    public void StartHardGame()
    {
        StartGame(GameDifficulty.HARD);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
