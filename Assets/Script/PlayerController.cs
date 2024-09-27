using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float fowardSpeed;

    private int desiredLane = 1; // di chuyển các làn 0 là làn trái 1 là giữa 2 là phải
    public float landDistance = 4; // khoang cach giua 2 làn
    public float jumpForce;

    public float Gravity = -20;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        direction.z = fowardSpeed;

        // Lấy ra đầu vào để di chuyển trên làn đường
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }
        Vector3 targetPossition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPossition += Vector3.left * landDistance;
        }
        else if (desiredLane == 2)
        {
            targetPossition += Vector3.right * landDistance;
        }
        transform.position = Vector3.Lerp(transform.position, targetPossition, 80 * Time.fixedDeltaTime);
    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }
}
