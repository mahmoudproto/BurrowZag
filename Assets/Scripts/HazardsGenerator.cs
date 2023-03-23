using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsGenerator : MonoBehaviour
{
    public GameObject hazardPrefap;
    public float hazardsDistanceOffset;
    public int minInterval=1, maxInterval=3;
    private int currentRandomInterval=2;
    private GameObject hazardsContainer;
    float TimePassedSenseLastHazard;
    Coroutine IncrementTimerCoroutine;
    private void Start()
    {
        hazardsContainer = new GameObject("Hazards Container");
        GameManager.gameResumedEvent += OnGameResumeHandler;
        GameManager.gamePausedEvent += OnGamePausedHandler;
    }

    private void OnGamePausedHandler()
    {
        StopCoroutine(IncrementTimerCoroutine);
    }

    private void OnGameResumeHandler()
    {
        IncrementTimerCoroutine = StartCoroutine(IncrementTimePassed());
    }

    void GenerateNewHazard()
    {
        Vector3 hazardPosition = GameManager.instance.playerTransform.position + new Vector3(0, hazardsDistanceOffset, 0);
        GameObject.Instantiate(hazardPrefap, hazardPosition, Quaternion.identity, hazardsContainer.transform);
        currentRandomInterval = Random.Range(minInterval,maxInterval);
    }

    IEnumerator IncrementTimePassed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.06f);
            TimePassedSenseLastHazard += 0.06f;
            if(TimePassedSenseLastHazard >= currentRandomInterval)
            {
                TimePassedSenseLastHazard = 0;
                GenerateNewHazard();
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.gameResumedEvent -= OnGameResumeHandler;
        GameManager.gamePausedEvent -= OnGamePausedHandler;
    }
}
