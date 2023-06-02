using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiamondsGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> diamondsPrefabs;
    [SerializeField] GameObject diamondsSpawnLocationsPrefab;
    [SerializeField] int diamondsDistancediffrence;
    List<GameObject[]> diamondsClusterBool;
    GameObject diamondsContainer;
    Transform playerTransform;
    GameObject diamondsSpawnLocationsParent;
    const int diamondsClusterCount = 3;
    List<Transform> diamondsSpawnLocationsList;
    private int previousRandomIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        diamondsContainer = new GameObject("Diamonds Container");
        HazardsGenerator.onNewHazardGenerated += GenerateDiamonds;
        playerTransform = GameManager.instance.playerTransform;
        InitializeDiamondsBool();
        IntializeDiamondsSpownLocations();
    }

    private void InitializeDiamondsBool()
    {
        GameObject tempDiamondPrefab;
        GameObject[] tempDiamondsArray;
        const int vertical_distance = 20;
        diamondsClusterBool = new List<GameObject[]>();
        for (int i = 0; i < diamondsPrefabs.Count; i++)
        {
            tempDiamondsArray = new GameObject[3];
            for (int j = 0; j < diamondsClusterCount; j++)
            {
                tempDiamondPrefab = Instantiate(diamondsPrefabs[i], Vector3.zero + new Vector3(0, vertical_distance), Quaternion.identity, diamondsContainer.transform);
                tempDiamondsArray[j] = tempDiamondPrefab;
            }
            diamondsClusterBool.Add(tempDiamondsArray);
        }
    }

    private void IntializeDiamondsSpownLocations()
    {
        const int vertical_distance = 20;
        diamondsSpawnLocationsParent = Instantiate(diamondsSpawnLocationsPrefab, new Vector3(0, vertical_distance), Quaternion.identity);
        diamondsSpawnLocationsList = new List<Transform>();
        foreach (Transform child in diamondsSpawnLocationsParent.transform)
        {
            diamondsSpawnLocationsList.Add(child);
        }
    }

    private void GenerateDiamonds()
    {
        ShuffleSpawnLocations();
        diamondsSpawnLocationsParent.transform.position = playerTransform.position - new Vector3(0, diamondsDistancediffrence);
        int randomIndex = Random.Range(0, diamondsClusterBool.Count);
        // remove this garbage whenever you can
        if (previousRandomIndex == randomIndex && (randomIndex == diamondsClusterBool.Count - 1 || randomIndex == 0))
            randomIndex = 2;
        else if(previousRandomIndex == randomIndex)
            randomIndex--;
        previousRandomIndex = randomIndex;
        ////////
        for (int i = 0; i < diamondsClusterCount; i++)
        {
            diamondsSpawnLocationsList[i].gameObject.SetActive(true);
            diamondsClusterBool[randomIndex][i].transform.position = diamondsSpawnLocationsList[i].position;
        }
    }

    private void ShuffleSpawnLocations()
    {
        System.Random randomObject = new System.Random();
        int remainingListItemsCount = diamondsSpawnLocationsList.Count;
        while (remainingListItemsCount > 1)
        {
            remainingListItemsCount--;
            int k = randomObject.Next(remainingListItemsCount + 1);
            Transform value = diamondsSpawnLocationsList[k];
            diamondsSpawnLocationsList[k] = diamondsSpawnLocationsList[remainingListItemsCount];
            diamondsSpawnLocationsList[remainingListItemsCount] = value;
        }
        // hide all diamond vines 
        for (int i = 0; i < diamondsSpawnLocationsList.Count; i++)
        {
            diamondsSpawnLocationsList[i].gameObject.SetActive(false);
        }
    }
}
