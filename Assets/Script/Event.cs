using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    public void ReloadGame(){
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame(){
        SceneManager.LoadScene("MainMenu");
    }
}
