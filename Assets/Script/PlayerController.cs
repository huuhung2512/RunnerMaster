using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float fowardSpeed;

    public float maxSpeed;
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private int desiredLane = 1; // di chuyển các làn 0 là làn trái 1 là giữa 2 là phải
    public float landDistance = 4; // khoang cach giua 2 làn
    public float jumpForce;
    private bool isSliding = false;
    public float Gravity = -20;
    private Animator animator;
    private Coroutine slideCoroutine;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        if (!PlayerManager.isGameStarted)
            return;
        //tăng tốc độ theo thời gian
        if (fowardSpeed < maxSpeed)
        {
            fowardSpeed += 0.2f * Time.deltaTime;
        }
        animator.SetBool("isGameStarted", true);
        animator.SetBool("isGrounded", isGrounded);
        direction.z = fowardSpeed;

        // Lấy ra đầu vào để di chuyển trên làn đường
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        if (isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                if (isSliding && slideCoroutine != null)
                {
                    // Hủy slide nếu đang trượt và nhảy ngay lập tức
                    StopCoroutine(slideCoroutine);
                    ResetSlide();
                    Jump();
                }
                else
                {
                    Jump();
                }
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }
        if (SwipeManager.swipeDown && !isSliding)
        {
            direction.y += Gravity * 100 * Time.deltaTime;
            slideCoroutine = StartCoroutine(Slide());
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
        // transform.position = Vector3.Lerp(transform.position, targetPossition, 80 * Time.fixedDeltaTime);
        // controller.center = controller.center;
        if (transform.position == targetPossition)
            return;
        Vector3 diff = targetPossition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }
    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, 0.11f, 0);
        controller.height = 0f;

        float elapsedTime = 0f;
        float slideDuration = 1.1f;

        // Kiểm tra thường xuyên nếu người chơi vuốt lên để hủy slide
        while (elapsedTime < slideDuration)
        {
            if (SwipeManager.swipeUp)
            {
                // Nếu vuốt lên thì dừng slide và nhảy ngay lập tức
                ResetSlide();
                Jump(); // Gọi hàm nhảy ngay lập tức
                yield break; // Dừng coroutine tại đây
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Chờ mỗi frame
        }

        // Sau thời gian trượt hoàn thành, đặt lại trạng thái nhân vật
        ResetSlide();
    }
    private void ResetSlide()
    {
        // Thiết lập lại trạng thái của nhân vật sau khi dừng slide
        controller.center = new Vector3(0, 0.4f, 0);
        controller.height = 0.8f;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}
