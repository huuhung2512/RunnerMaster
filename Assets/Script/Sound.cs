using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sound : MonoBehaviour
{
    public string nameAudio;
    public AudioClip audioClip;
    public float volume;
    public bool loop;
    public AudioSource audioSource;
}
