using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animator))]
public class AnimationEvent : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

}
