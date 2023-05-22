using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameisPaused => gamePaused;
    private bool gamePaused;
    [HideInInspector] public float scoreMultiplier=1;
    [SerializeField] private float maxSpeedMultiplier;
    [SerializeField] private StageInformations[] stagesInfo;

    [SerializeField] TMPro.TMP_Text score_text;
    private float score = 0;
    Coroutine ScoreandTimeCoroutine;

    [HideInInspector] public Transform playerTransform;
    private float timePassed;

    public static event Action onGamePaused;
    public static event Action onGameResumed;
    public static event Action<float> onGameOver;
    public static event Action<StageInformations> onStageChangeEvent;
    public static event Action<float> OnEnergyPackHit;
    public static event Action<Collectible> OnNewEnergyPackGenerated;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(this);
        gamePaused = true;
    }

    public void Fire_EnergyPack_Hit_event(float energy)
    {
        OnEnergyPackHit?.Invoke(energy);
    }
    int lastIndex=0;
    public void Fire_NewEnergyPackGenerated(Collectible collectable)
    {
        OnNewEnergyPackGenerated?.Invoke(collectable);
        collectable.MyIndex = lastIndex++;
    }

    public void intialzeNewRun()
    {
        StartStagesCoroutines();
        ResumeGame();
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

    IEnumerator ChangeStageAfter(float seconds,StageInformations stageInfo)
    {
        yield return new WaitForSeconds(seconds);
        ChangeStage(stageInfo);
    }

    private void ChangeStage(StageInformations stage)
    {
        onStageChangeEvent?.Invoke(stage);
        scoreMultiplier = stage.speed_multiplier;
        playerTransform.GetComponent<TrailRenderer>().colorGradient = stage.trail_color;
    }

    public void PlayerHitHazard()
    {
        PauseGame();
        onGameOver?.Invoke(score);
    }

    private void OnDestroy()
    {
        onGameOver = null;
        onGamePaused = null;
        onGameResumed = null;
        onStageChangeEvent = null;
    }

}

[Serializable]
public struct StageInformations
{
    public int start_time;
    public float speed_multiplier;
    public Gradient trail_color;
}
