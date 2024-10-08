using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
public class MainMenu : MonoBehaviour
{
   public TextMeshProUGUI coinsText;
   public void PlayGame(){
        SceneManager.LoadScene("Level 1");
   }
   void Start()
{
    int totalCoins = PlayerPrefs.GetInt("Coins", 0);
    Debug.Log("Số coin hiển thị trong MainMenu: " + totalCoins);  
    coinsText.text =  totalCoins.ToString();
}
}
