using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public string stats;

    public void Awake()
    {
        int minutes = Mathf.FloorToInt(Game.instance.playTime / 60f);
        int seconds = Mathf.FloorToInt(Mathf.Repeat(Game.instance.playTime, 60f));
        this.stats = string.Format("Time played: {0} minute(s) {1} second(s)", minutes, seconds);
    }
}
