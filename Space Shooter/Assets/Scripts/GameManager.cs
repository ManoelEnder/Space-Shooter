using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    public int playerLives = 3;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int p)
    {
        score += p;
        UpdateUI();
    }
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void PlayerHit()
    {
        playerLives--;
        if (playerLives <= 0) GameOver();
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
}