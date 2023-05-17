using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsGenerator : MonoBehaviour
{
    [SerializeField] private bool testingNewHazzard;
    [SerializeField] private GameObject newHazzard;
    [Space(10)]
    public List<GameObject> hazardsPrefaps;
    [SerializeField] private float extraHazardDistance;
    private List<GameObject> hazardsBool;
    [Space(10)]
    [SerializeField] private float minInterval, maxInterval;
    [SerializeField] private float currentRandomInterval;
    private GameObject hazardsContainer;
    private float TimePassedSenseLastHazard;
    private Coroutine IncrementTimerCoroutine;
    private int previous_randomHazardIndex = -1;

    private void Start()
    {
        hazardsContainer = new GameObject("Hazards Container");
        GameManager.onGameResumed += OnGameResumeHandler;
        GameManager.onGamePaused += OnGamePausedHandler;
        GameObject tempHazard;
        const int vertical_distance = 20;
        hazardsBool = new List<GameObject>();
        for (int i = 0; i < hazardsPrefaps.Count; i++)
        {
            tempHazard = GameObject.Instantiate(hazardsPrefaps[i], Vector3.zero + new Vector3(0, vertical_distance), Quaternion.identity, hazardsContainer.transform);
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
        //get a new random index if it's the same as the previous hazard #lazy solution
        if (previous_randomHazardIndex == randomHazardIndex)
            randomHazardIndex = Random.Range(0, hazardsBool.Count);
        previous_randomHazardIndex = randomHazardIndex;
        GameObject randomHazard = hazardsBool[randomHazardIndex];
        if (testingNewHazzard)
            randomHazard = GameObject.Instantiate(newHazzard, Vector3.zero , Quaternion.identity, hazardsContainer.transform); ;
        Vector3 hazardPosition = GameManager.instance.playerTransform.position + hazardsPrefaps[randomHazardIndex].transform.position - new Vector3(0,extraHazardDistance);
        randomHazard.transform.position = hazardPosition;
        currentRandomInterval = Random.Range(minInterval, maxInterval + 1);
        randomHazard.GetComponentInChildren<CollectibleRandomizer>().Randomize();
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
