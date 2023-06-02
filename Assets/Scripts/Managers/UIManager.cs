using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject startNewGameOverlay;
    public TMPro.TMP_Text score_text;
    public TMPro.TMP_Text gemsCount_text;
    int gems;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onGameOver += GameOver_Handler;
        DiamondController.onPlayerHitDiamond += IncreeseDiamonds;
    }

    private void IncreeseDiamonds()
    {
        gems ++;
        gemsCount_text.text = gems.ToString();
    }

    private float deltaTime;
    [SerializeField] TMPro.TMP_Text FPS_text;
    //private void Update()
    //{
    //    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    //    float fps = 1.0f / deltaTime;
    //    //Application.targetFrameRate = 60;
    //    FPS_text.text = "FPS : " + Mathf.Ceil(fps).ToString();// + "\nTarget = " + Application.targetFrameRate;
    //}

    void GameOver_Handler(float score)
    {
        gameOverScreen.SetActive(true);
        score_text.text = ((int)score).ToString();
    }

    //get's called on pointer down of the "Start New Game overlay" located in the HUD canvas 
    public void StartNewGame()
    {
        startNewGameOverlay.SetActive(false);
        GameManager.instance.intialzeNewRun();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        GameManager.onGameOver -= GameOver_Handler;
    }
}
