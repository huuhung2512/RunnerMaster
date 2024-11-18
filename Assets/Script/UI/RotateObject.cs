using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject[] targetObjects; 
    public float rotationSpeed = 50f; 

    void Update()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                obj.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            }
        }
    }
}
