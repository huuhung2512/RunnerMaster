using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        transform.Rotate(50*Time.deltaTime, 0,0);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            audioManager.PlaySFX(audioManager.pickUpCoin);
            PlayerManager.numberOfCoin += 1;
            Destroy(gameObject);
        }
    }
}
