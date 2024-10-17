using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public static bool isPaused;    // Biến để kiểm tra xem game có bị tạm dừng hay không
    public GameObject gameOverPanel;
    public GameObject tabToStart;
    public static int numberOfCoin;
    public static float playerScore;

    public Transform player;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI bestScoreText;  // Hiển thị best score
    public TextMeshProUGUI scoreText;      // Hiển thị điểm số khi kết thúc
    [SerializeField] private Animator scoreTextAnim;
    [SerializeField] private Animator bestScoreTextAnim;
    private float startingZ;    // Vị trí bắt đầu của player trên trục Z
    private float bestScore;    // Điểm cao nhất đã lưu

    void Start()
    {
        ResetGameState();
        LoadBestScore();
        startingZ = player.position.z;  // Lưu lại vị trí Z ban đầu của player
    }

    void Update()
    {
        if (isPaused || gameOver) return;  // Nếu game đang bị tạm dừng hoặc game over thì không làm gì cả

        if (isGameStarted)
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
        isPaused = false;  // Khởi tạo trạng thái không tạm dừng
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
        bestScoreText.text = "Best Score:\n " + bestScore.ToString("F0") + " m";
    }

    public void HandleGameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        coinText.gameObject.SetActive(false);
        distanceText.gameObject.SetActive(false);
        SavePlayerData();
        AudioManager.Instance.StopMusicPlay();
        // Hiển thị điểm số cuối cùng của người chơi
        scoreText.text = "Score:\n " + playerScore.ToString("F0") + " m";
        if (playerScore > bestScore)
        {
            scoreTextAnim.speed = 0;
            bestScoreTextAnim.speed = 1;
            Debug.Log("dang bat anim bestScore");
        }
        else
        {
            scoreTextAnim.speed = 1;
            bestScoreTextAnim.speed = 0;
            Debug.Log("0 bat anim bestScore");
        }
    }

    void UpdatePlayerScore()
    {
        playerScore = player.position.z - startingZ;
        distanceText.text = playerScore.ToString("F0") + " m";
    }

    void StartGame()
    {
        isGameStarted = true;
        tabToStart.SetActive(false);
        AudioManager.Instance.PlayGameMusic();
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
            bestScoreText.text = "Best Score: " + bestScore.ToString("F0") + " m";

            PlayerPrefs.SetFloat("BestScore", bestScore);
        }
        int totalCoins = PlayerPrefs.GetInt("Coins", 0);
        totalCoins += numberOfCoin;
        PlayerPrefs.SetInt("Coins", totalCoins);
        PlayerPrefs.Save();
    }

    // Hàm để dừng game thủ công
    public void PauseGame()
    {
        isPaused = true;
        AudioManager.Instance.StopMusicPlay();  // Tạm dừng nhạc khi tạm dừng game
    }

    // Hàm để tiếp tục game sau khi tạm dừng
    public void ResumeGame()
    {
        isPaused = false;
        AudioManager.Instance.PlayGameMusic();  // Tiếp tục nhạc khi tiếp tục game
    }
}
