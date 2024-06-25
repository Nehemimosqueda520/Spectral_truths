using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGamePaused = false;
    void Update()
    {
        Time.timeScale = isGamePaused ? 0f : 1f;
    }

    public void TogglePausedGame()
    {
        isGamePaused = !isGamePaused;
    }
}
