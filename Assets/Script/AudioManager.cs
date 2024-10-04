using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------AUDIO SOURCE-----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("----------AUDIO CLIP--------------------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip pickUpCoin;
    private void Start() {
        musicSource.clip = background;
    }
    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
    public void PlayMusic(){
        musicSource.Play();
    }
    public void StopMusic(){
        if(musicSource.isPlaying){
            musicSource.Stop();
        }
    }
}
