using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameisPaused => gamePaused;
    private bool gamePaused;
    [HideInInspector] public float speedMultiplier=1;
    [SerializeField] private float maxSpeedMultiplier;
    [SerializeField] private Gradient[] speedColors;
    [SerializeField] private ValuePair[] SpeedTimePoints;

    [SerializeField] TMPro.TMP_Text score_text;
    public float score = 0;
    Coroutine scoreCoroutine;

    public Coroutine IncreaseSpeedCoroutine { get; private set; }

    [HideInInspector] public Transform playerTransform;
    private float timePassed;

    public static event Action gamePausedEvent;
    public static event Action gameResumedEvent;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        gamePaused = true;
        speedMultiplier = 1;
    }
    public void PauseGame()
    {
        gamePaused = true;
        gamePausedEvent?.Invoke();
        StopCoroutine(scoreCoroutine);
    }
    public void ResumeGame()
    {
        if (gamePaused == false)
            return;
        gamePaused = false;
        gameResumedEvent?.Invoke();
        scoreCoroutine = StartCoroutine(IncrementScoreandTime());
    }
    IEnumerator IncrementScoreandTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            timePassed += .1f;
            score += speedMultiplier;
            score_text.text =((int)score).ToString();
        }
    }

    private void IncreaseSpeedMultiplier()
    {
        if (timePassed < SpeedTimePoints[0].value1)
            return;
        else if (timePassed >= SpeedTimePoints[2].value2)
        {
           //speedMultiplier=
        }
    }

    public void PlayerHitHazard()
    {
        SceneManager.LoadScene(0);
    }
}
[Serializable]
public class ValuePair
{
    public int value1;
    public float value2;
}
