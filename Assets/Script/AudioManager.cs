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
    [SerializeField] private Button musicButton; 
    [SerializeField] private Button sfxButton;   
    private void Start()
    {
        PlayMenuMusic();
        musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF";
        sfxButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF";
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

    // Hàm để bật/tắt nhạc
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
        {
            PlayMenuMusic();
            musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF"; // Cập nhật nhãn
        }
        else
        {
            StopMusicPlay();
            StopMusicMenu();
            musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "ON"; // Cập nhật nhãn
        }
    }

    public void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
        sfxButton.GetComponentInChildren<TextMeshProUGUI>().text = isSFXOn ? "OFF" : "ON"; // Cập nhật nhãn
    }
}
