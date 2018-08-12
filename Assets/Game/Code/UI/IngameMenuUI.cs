using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenuUI : MonoBehaviour
{
    private bool paused;

    public void OnEnable()
    {
        this.paused = !Game.instance.isPaused;
        if (paused)
            Game.instance.TogglePause();
    }

    public void OnDisable()
    {
        if (paused)
            Game.instance.TogglePause();
        paused = false;
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
