using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject[] targetObjects; 
    public float rotationSpeed = 5f; 
    private Quaternion[] targetRotations;

    void Start()
    {
        targetRotations = new Quaternion[targetObjects.Length];
        for (int i = 0; i < targetObjects.Length; i++)
        {
            targetRotations[i] = targetObjects[i].transform.rotation; 
        }
    }

    void Update()
    {
        if (SwipeManager.swipeLeft)
        {
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targetRotations[i] *= Quaternion.Euler(0, 30, 0);
            }
        }
        if (SwipeManager.swipeRight)
        {
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targetRotations[i] *= Quaternion.Euler(0, -30, 0);
            }
        }

        for (int i = 0; i < targetObjects.Length; i++)
        {
            targetObjects[i].transform.rotation = Quaternion.Lerp(
                targetObjects[i].transform.rotation, 
                targetRotations[i], 
                Time.deltaTime * rotationSpeed
            );
        }
    }
}
