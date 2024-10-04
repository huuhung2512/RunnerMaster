using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public GameObject gameOverPanel;
    public GameObject tabToSart;
    public static int numberOfCoin;

    public TextMeshProUGUI coinText;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        Time.timeScale = 1;
        numberOfCoin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            // Dừng nhạc nền khi gameover
            audioManager.StopMusic();
        }
        coinText.text = "Foods: " + numberOfCoin ;
        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            tabToSart.SetActive(false);

            // Bật nhạc khi user nhấn zo màn hình lần đầu
            audioManager.PlayMusic();
        }
    }
}
