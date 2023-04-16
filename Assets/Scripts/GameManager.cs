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
    [HideInInspector] public float scoreMultiplier=1;
    [SerializeField] private float maxSpeedMultiplier;
    [SerializeField] private StageInformations[] stagesInfo;

    [SerializeField] TMPro.TMP_Text score_text;
    public float score = 0;
    Coroutine ScoreandTimeCoroutine;

    [HideInInspector] public Transform playerTransform;
    private float timePassed;

    public static event Action onGamePaused;
    public static event Action onGameResumed;
    public static event Action<StageInformations> onStageChangeEvent;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        gamePaused = true;
        StartStagesCoroutines();
    }

    private void StartStagesCoroutines()
    {
        for (int i = 0; i < stagesInfo.Length; i++)
        {
            StartCoroutine(ChangeStageAfter(stagesInfo[i].start_time, stagesInfo[i]));
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        onGamePaused?.Invoke();
        StopCoroutine(ScoreandTimeCoroutine);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        if (gamePaused == false)
            return;
        gamePaused = false;
        onGameResumed?.Invoke();
        ScoreandTimeCoroutine = StartCoroutine(IncrementScoreandTime());
        Time.timeScale = 1;
    }

    IEnumerator IncrementScoreandTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            timePassed += .1f;
            score += scoreMultiplier;
            score_text.text =((int)score).ToString();
        }
    }

    IEnumerator ChangeStageAfter(float secounds,StageInformations stageInfo)
    {
        yield return new WaitForSeconds(secounds);
        ChangeStage(stageInfo);
        Debug.Log("stage started at "+ timePassed);
    }

    private void ChangeStage(StageInformations stage)
    {
        onStageChangeEvent?.Invoke(stage);
        scoreMultiplier = stage.speed_multiplier;
        playerTransform.GetComponent<TrailRenderer>().colorGradient = stage.trail_color;
    }

    public void PlayerHitHazard()
    {
        SceneManager.LoadScene(0);
    }
}
[Serializable]
public struct StageInformations
{
    public int start_time;
    public float speed_multiplier;
    public Gradient trail_color;
}
