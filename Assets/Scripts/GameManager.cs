using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GamePaused => gamePaused;
    private bool gamePaused;
    [HideInInspector]
    public Transform playerTransform;
    public static event Action gamePausedEvent;
    public static event Action gameResumedEvent;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        gamePaused = true;
    }

    public void PauseGame()
    {
        gamePaused = true;
        gamePausedEvent?.Invoke();
    }

    public void ResumeGame()
    {
        if (gamePaused == false)
            return;
        gamePaused = false;
        gameResumedEvent?.Invoke();
    }
    public void PlayerHitHazard()
    {
        SceneManager.LoadScene(0);
    }
}
