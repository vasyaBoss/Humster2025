using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class PlayerController : MonoBehaviour
{
    [Header("Movement & Jump")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("References")]
    public Transform rabbitMesh;
    public Camera playerCamera;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Slider healthSlider;
    public GameOverUIController gameOverUIController;

    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Audio")]
    public AudioClip runSound;
    public AudioClip damageSound;
    public AudioClip jumpSound;

    private Rigidbody rb;
    private Animator animator;
    private AudioSource audioSource;

    private bool isGrounded;
    private bool isRunning;
    private bool isDead;
    private Vector3 spawnPoint;

    // Mobile input state
    private Vector2 swipeDelta;
    private bool tapped;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        animator = rabbitMesh.GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        spawnPoint = transform.position;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void OnEnable()
    {
        LeanTouch.OnFingerSwipe += HandleFingerSwipe;
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= HandleFingerSwipe;
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    void Update()
    {
        if (isDead) return;

        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        // Determine movement vector
        Vector3 move = Vector3.zero;

        if (Application.isMobilePlatform)
        {
            // Mobile: swipe to move
            move = new Vector3(swipeDelta.x, 0f, swipeDelta.y);

            // Tap to jump
            if (tapped && isGrounded)
            {
                DoJump();
            }

            tapped = false;
            swipeDelta = Vector2.zero;
        }
        else
        {
            // PC: keyboard input
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 forward = playerCamera.transform.forward;
            Vector3 right = playerCamera.transform.right;
            forward.y = right.y = 0f;
            forward.Normalize();
            right.Normalize();

            move = (forward * moveZ + right * moveX).normalized;

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                DoJump();
            }
        }

        // Running animation & sound
        float speedValue = move.magnitude * moveSpeed;
        animator.SetFloat("Speed", speedValue);

        if (move.magnitude > 0.1f && isGrounded && !isRunning)
        {
            isRunning = true;
            PlayRunSound();
        }
        else if ((move.magnitude <= 0.1f || !isGrounded) && isRunning)
        {
            isRunning = false;
            StopRunSound();
        }

        // Apply movement
        MoveCharacter(move);

        // Update health UI
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    private void MoveCharacter(Vector3 move)
    {
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            rabbitMesh.rotation = Quaternion.Slerp(rabbitMesh.rotation, targetRotation, Time.deltaTime * 10f);
        }

        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }

    private void DoJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        PlaySound(jumpSound);
        animator.SetTrigger("Jump");
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Max(0f, currentHealth - damage);
        PlaySound(damageSound);
        animator.SetTrigger("Hit");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        gameOverUIController.ShowGameOverUI();
        rb.isKinematic = true;
    }

    public void Respawn()
    {
        transform.position = spawnPoint;
        rb.isKinematic = false;
        currentHealth = maxHealth;
        isDead = false;
        animator.ResetTrigger("Death");
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    // Lean Touch callbacks
    private void HandleFingerSwipe(LeanFinger finger)
    {
        swipeDelta = finger.SwipeScreenDelta.normalized;
    }

    private void HandleFingerTap(LeanFinger finger)
    {
        tapped = true;
    }

    // Audio helpers
    private void PlayRunSound()
    {
        if (runSound != null)
        {
            audioSource.clip = runSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void StopRunSound()
    {
        audioSource.Stop();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
