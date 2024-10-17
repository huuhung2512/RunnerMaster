using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    public void ReloadGame(){
        SceneManager.LoadScene("Level 1");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
    }
    public void QuitGame(){
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClick);
        AudioManager.Instance.PlayMenuMusic();
    }
}
