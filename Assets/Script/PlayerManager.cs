using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public GameObject gameOverPanel;
    public GameObject tabToStart;
    public static int numberOfCoin;
    public static float playerScore;

    public Transform player;

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI bestScoreText;  // Hiển thị best score
    public TextMeshProUGUI scoreText;      // Hiển thị điểm số khi kết thúc
    public TextMeshProUGUI coinText;

    private AudioManager audioManager;

    private float startingZ;    // Vị trí bắt đầu của player trên trục Z
    private float bestScore;    // Điểm cao nhất đã lưu

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        ResetGameState();
        LoadBestScore();
        startingZ = player.position.z;  // Lưu lại vị trí Z ban đầu của player
    }

    void Update()
    {
        if (gameOver)
        {
           // HandleGameOver();
        }
        else if (isGameStarted)
        {
            UpdatePlayerScore();
        }

        if (SwipeManager.tap && !isGameStarted)
        {
            StartGame();
        }

        // Cập nhật số coins
        UpdateCoinText();
    }

    void ResetGameState()
    {
        gameOver = false;
        isGameStarted = false;
        Time.timeScale = 1;
        numberOfCoin = 0;
        playerScore = 0;
        gameOverPanel.SetActive(false);
        distanceText.gameObject.SetActive(true);
        coinText.gameObject.SetActive(true);
    }

    void LoadBestScore()
    {
        // Lấy best score đã lưu từ PlayerPrefs
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
        bestScoreText.text = "Best Score:\n " + bestScore.ToString("F2") + " m";
    }

    public void HandleGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        coinText.gameObject.SetActive(false);
        distanceText.gameObject.SetActive(false);
        SavePlayerData();
        audioManager.StopMusic();
        // Hiển thị điểm số cuối cùng của người chơi
        scoreText.text = "Score:\n " + playerScore.ToString("F2") + " m";
    }

    void UpdatePlayerScore()
    {
        playerScore = player.position.z - startingZ;
        distanceText.text = playerScore.ToString("F2") + " m";
    }

    void StartGame()
    {
        isGameStarted = true;
        tabToStart.SetActive(false);
        audioManager.PlayMusic();
    }

    void UpdateCoinText()
    {
        coinText.text = "Golds: " + numberOfCoin;
    }

    void SavePlayerData()
    {
        if (playerScore > bestScore)
        {
            bestScore = playerScore;
            bestScoreText.text = "Best Score: " + bestScore.ToString("F2") + " m";
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }
        int totalCoins = PlayerPrefs.GetInt("Coins", 0);
        totalCoins += numberOfCoin;
        PlayerPrefs.SetInt("Coins", totalCoins);
        PlayerPrefs.Save();  
    }
}
