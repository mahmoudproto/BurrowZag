using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsGenerator : MonoBehaviour
{
    [SerializeField] private bool testingNewHazzard;
    [SerializeField] private GameObject newHazzard;
    [Space(10)]
    public List<GameObject> hazardsPrefaps;
    private List<GameObject> hazardsBool;
    [Space(10)]
    public float minInterval, maxInterval;
    private float currentRandomInterval;
    private GameObject hazardsContainer;
    float TimePassedSenseLastHazard;
    Coroutine IncrementTimerCoroutine;

    private void Start()
    {
        hazardsContainer = new GameObject("Hazards Container");
        GameManager.onGameResumed += OnGameResumeHandler;
        GameManager.onGamePaused += OnGamePausedHandler;
        GameObject tempHazard;
        hazardsBool = new List<GameObject>();
        for (int i = 0; i < hazardsPrefaps.Count; i++)
        {
            tempHazard = GameObject.Instantiate(hazardsPrefaps[i], Vector3.zero+new Vector3(0,20), Quaternion.identity, hazardsContainer.transform);
            hazardsBool.Add(tempHazard);
        }
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
        int randomHazardIndex = Random.Range(0, hazardsBool.Count);
        GameObject randomHazard = hazardsBool[randomHazardIndex];
        if (testingNewHazzard)
            randomHazard = newHazzard;
        Vector3 hazardPosition = GameManager.instance.playerTransform.position + hazardsPrefaps[randomHazardIndex].transform.position;
        randomHazard.transform.position = hazardPosition;
        //GameObject.Instantiate(randomHazard, hazardPosition, Quaternion.identity, hazardsContainer.transform);
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
        GameManager.onGameResumed -= OnGameResumeHandler;
        GameManager.onGamePaused -= OnGamePausedHandler;
    }
}
