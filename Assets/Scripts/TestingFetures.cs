using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingFetures : MonoBehaviour
{
    public GameObject[] MovementVisualizaers; 
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MovementVisualizaers.Length; i++)
        {
            MovementVisualizaers[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputController.gamePaused = !InputController.gamePaused;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

    }
}
