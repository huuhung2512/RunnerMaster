using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public AudioClip buttonClick;
    public AudioClip buying;
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
    public void Buying(){
        SFXSource.PlayOneShot(buying);
    }
    public void ButtonClick(){
        SFXSource.PlayOneShot(buttonClick);
    }
}
