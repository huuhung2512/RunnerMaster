using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] PlayerManager playerManager;
    private Vector3 direction;
    public float fowardSpeed = 8;
    public float maxSpeed;
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private int desiredLane = 1;
    public float laneDistance = 2.5f;
    public float jumpForce = 10;
    private bool isSliding = false;
    private bool isFalling = false;
    public float gravity = -20;
    public Animator animator;
    private Coroutine slideCoroutine;
    private AudioManager audioManager;

    private const float slideDuration = 1.1f;
    private const float groundCheckRadius = 0.15f;
    private const float speedIncreaseRate = 0.2f;
    private const float slideGravityMultiplier = 100;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        UpdateAnimation();

        if (fowardSpeed < maxSpeed)
        {
            fowardSpeed += speedIncreaseRate * Time.deltaTime;
        }

        direction.z = fowardSpeed;

        // Di chuyển trên làn đường
        if (SwipeManager.swipeRight)
        {
            desiredLane = Mathf.Min(desiredLane + 1, 2);
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane = Mathf.Max(desiredLane - 1, 0);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            isFalling = false;
            if (SwipeManager.swipeUp)
            {
                HandleJumpWhileSliding();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
            isFalling = direction.y < 0;
        }

        if (SwipeManager.swipeDown && !isSliding)
        {
            direction.y += gravity * slideGravityMultiplier * Time.deltaTime;
            slideCoroutine = StartCoroutine(Slide());
        }
        MoveToDesiredLane();
    }

    private void MoveToDesiredLane()
    {
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        controller.Move(moveDir.sqrMagnitude < diff.sqrMagnitude ? moveDir : diff);
    }

    private void FixedUpdate()
    {
        if (PlayerManager.isGameStarted)
        {
            controller.Move(direction * Time.fixedDeltaTime);
        }
    }

    private void UpdateAnimation()
    {
        SetAnimatorBool("isGameStarted", true);
        SetAnimatorBool("isGrounded", isGrounded);
        SetAnimatorBool("isFall", isFalling);
    }

    private void SetAnimatorBool(string parameter, bool value)
    {
        if (animator.GetBool(parameter) != value)
        {
            animator.SetBool(parameter, value);
        }
    }
    //nếu player lướt lên sẽ nhảy luôn
    private void HandleJumpWhileSliding()
    {
        if (isSliding && slideCoroutine != null)
        {
            StopCoroutine(slideCoroutine);
            ResetSlide();
            Jump();
            isSliding = false;
        }
        else
        {
            Jump();
        }
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            PlayerManager.gameOver = true;
            playerManager.HandleGameOver();
            audioManager.PlaySFX(audioManager.death);
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        isFalling = false;
        SetAnimatorBool("isSliding", true);
        controller.center = new Vector3(0, 0.225f, 0);
        controller.height = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            if (SwipeManager.swipeUp && isGrounded)
            {
                ResetSlide();
                Jump();
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ResetSlide();
    }

    private void ResetSlide()
    {
        controller.center = new Vector3(0, 0.4f, 0);
        controller.height = 0.8f;
        isSliding = false;
        SetAnimatorBool("isSliding", false);
    }
}
