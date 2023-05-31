using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiamondsGenerator : MonoBehaviour
{
    [SerializeField]List<GameObject> diamondsPrefaps;
    [SerializeField] GameObject diamondsSpawnLocations;
    [SerializeField] int diamondsDistancediffrence;
    List<GameObject[]> diamondsClusterBool;
    GameObject diamondsContainer;
    Transform playerTransform;
    const int diamondsClusterCount = 3;
    // Start is called before the first frame update
    void Start()
    {
        diamondsContainer = new GameObject("Diamonds Container");
        HazardsGenerator.onNewHazardGenerated += GenerateDiamonds;
        playerTransform =GameManager.instance.playerTransform;
        InitializeDiamondsBool();
    }

    private void InitializeDiamondsBool()
    {
        GameObject tempDiamondPrefab;
        GameObject[] tempDiamondsArray;
        const int vertical_distance = 20;
        diamondsClusterBool = new List<GameObject[]>();
        for (int i = 0; i < diamondsPrefaps.Count; i++)
        {
            tempDiamondsArray = new GameObject[3];
            for (int j = 0; j < diamondsClusterCount; j++)
            {
                tempDiamondPrefab = GameObject.Instantiate(diamondsPrefaps[i], Vector3.zero + new Vector3(0, vertical_distance), Quaternion.identity, diamondsContainer.transform);
                tempDiamondsArray[j] = tempDiamondPrefab;
            }
            diamondsClusterBool.Add(tempDiamondsArray);
        }
    }

    private void GenerateDiamonds()
    {
        int randomIndex = Random.Range(0, diamondsClusterBool.Count);
        for (int i = 0; i < diamondsClusterCount; i++)
        {
            diamondsClusterBool[randomIndex][i].transform.position = playerTransform.position - new Vector3(0,diamondsDistancediffrence);
        }

    }

}
