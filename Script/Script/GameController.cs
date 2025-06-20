using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Text scoreText;
    public Text gameOverText;
    public GameObject gameOverPanel;
    public bool isGameOver = false;
    private int score = 0;
   

   
    

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

    }

    void Update()
    {
        
    }

    public void AddScoreAnh(int points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            Debug.Log("AddScore called! Score = " + score);
        }
    }

    public void GameOverAnh()
    {
        if (gameOverText != null && gameOverPanel != null)
        {
            gameOverText.text = "Your Score: " + score;
            gameOverPanel.SetActive(true);
            isGameOver = true;
            Time.timeScale = 0;
        }
    }
    public void NewStageAnh()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
