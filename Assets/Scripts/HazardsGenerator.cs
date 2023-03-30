using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsGenerator : MonoBehaviour
{
    public List<GameObject> easyHazards, mediumHazards, hardHazards;
    [Space(10)]
    public float minInterval, maxInterval;
    private float currentRandomInterval;
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
        GameObject randomHazard = easyHazards[Random.Range(0, easyHazards.Count)];
        Vector3 hazardPosition = GameManager.instance.playerTransform.position + randomHazard.transform.position;
        GameObject.Instantiate(randomHazard, hazardPosition, Quaternion.identity, hazardsContainer.transform);
        currentRandomInterval = Random.Range(minInterval, maxInterval + 1);
    }

    IEnumerator IncrementTimePassed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            TimePassedSenseLastHazard += 0.1f;
            if (TimePassedSenseLastHazard >= currentRandomInterval)
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
