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
    public GameObject victoryPanel;
    public int scoreToWin = 1000;

    bool gameEnded = false;
    GameObject player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        UpdateUI();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
    }

    public void AddScore(int p)
    {
        if (gameEnded) return;

        score += p;
        UpdateUI();

        if (score >= scoreToWin) Victory();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    public void PlayerHit()
    {
        if (gameEnded) return;

        playerLives--;
        if (playerLives <= 0) GameOver();
    }

    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (SongManager.Instance != null)
            SongManager.Instance.FadeOutAndStop(1.2f);

        if (player != null)
            Destroy(player);

        Time.timeScale = 0f;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void Victory()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (SongManager.Instance != null)
            SongManager.Instance.FadeOutAndStop(1.2f);

        if (player != null)
            Destroy(player);

        Time.timeScale = 0f;
        if (victoryPanel != null) victoryPanel.SetActive(true);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        if (SongManager.Instance != null) SongManager.Instance.RestoreAndPlay();
        SceneManager.LoadScene("Main");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        if (SongManager.Instance != null) SongManager.Instance.RestoreAndPlay();
        SceneManager.LoadScene("MainMenu");
    }
}