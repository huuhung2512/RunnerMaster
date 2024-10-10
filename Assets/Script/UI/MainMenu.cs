using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    void Start()
    {
        int totalCoins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = totalCoins.ToString();
    }

}
