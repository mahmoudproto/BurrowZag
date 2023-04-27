using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject startNewGameOverlay;
    public TMPro.TMP_Text score_text;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onGameOver += GameOver_Handler;
    }

    void GameOver_Handler(float score)
    {
        gameOverScreen.SetActive(true);
        score_text.text = ((int)score).ToString();
    }

    public void StartNewGame()
    {
        startNewGameOverlay.SetActive(false);
        GameManager.instance.intialzeNewRun();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        GameManager.onGameOver -= GameOver_Handler;
    }
}
