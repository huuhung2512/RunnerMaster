using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : Singleton<AudioManager>
{
    [Header("----------AUDIO SOURCE-----------------")]
    [SerializeField] AudioSource musicSourcePlay;
    [SerializeField] AudioSource musicSourceMenu;
    [SerializeField] AudioSource SFXSource;

    [Header("----------AUDIO CLIP--------------------")]
    public AudioClip background;
    public AudioClip backgroundMenu;
    public AudioClip death;
    public AudioClip pickUpCoin;
    public AudioClip buttonClick;
    public AudioClip buying;

    // Trạng thái âm thanh
    private bool isMusicOn = true;
    private bool isSFXOn = true;
    private void Start()
    {
        PlayMenuMusic();
    }
   
    public void PlaySFX(AudioClip clip)
    {
        if (isSFXOn) // Kiểm tra trạng thái âm thanh hiệu ứng
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    public void PlayGameMusic()
    {
        if (isMusicOn)
        {
            StopMusicMenu();
            musicSourcePlay.clip = background;
            musicSourcePlay.Play();
        }
    }

    public void PlayMenuMusic()
    {
        if (isMusicOn)
        {
            StopMusicPlay();
            musicSourceMenu.clip = backgroundMenu;
            musicSourceMenu.Play();
        }
    }

    public void StopMusicPlay()
    {
        if (musicSourcePlay.isPlaying)
        {
            musicSourcePlay.Stop();
        }
    }

    public void StopMusicMenu()
    {
        if (musicSourceMenu.isPlaying)
        {
            musicSourceMenu.Stop();
        }
    }

    public void Buying()
    {
        PlaySFX(buying);
    }

    public void ButtonClick()
    {
        PlaySFX(buttonClick);
    }

    // Hàm để ON/OFF nhạc
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("MusicSetting", isMusicOn ? 1 : 0);
        if (isMusicOn)
        {
            PlayMenuMusic();
            Debug.Log("Nhac bat");
        }
        else
        {
            StopMusicPlay();
            StopMusicMenu();
            Debug.Log("Nhac tat");
        }
        string musicText = isMusicOn ? "OFF" : "ON";
        PanelSettingUI.Instance.SetTextMusic(musicText);
        PlayerPrefs.SetString("MusicText", musicText);
        PlayerPrefs.Save();
    }

    public void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
        PlayerPrefs.SetInt("SFXSetting", isSFXOn ? 1 : 0);
        string sfxText = isSFXOn ? "OFF" : "ON";
        PanelSettingUI.Instance.SetTextSound(sfxText);
        PlayerPrefs.SetString("SFXText", sfxText);
        PlayerPrefs.Save();
    }
}
