using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Áudio (SFX)")]
    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    [Range(0f, 1f)] public float deathSoundVolume = 0.5f;

    [Header("Parâmetros do Som de Passos")]
    public float stepInterval = 0.3f; 
    private float stepTimer;

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;

    private float moveInput;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;
        moveInput = 0;
        if (Input.GetKey(KeyCode.A)) moveInput = -1;
        if (Input.GetKey(KeyCode.D)) moveInput = 1;
        
        bool running = moveInput != 0;
        anim.SetBool("isRunning", running);

        bool grounded = IsGrounded();
        anim.SetBool("isGrounded", grounded);

        HandleStepSounds(moveInput, grounded);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if (moveInput > 0)
            transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
    }

    void FixedUpdate()
    {
        if (isDead) return;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    public void KillPlayer()
    {
        if (isDead) return;
        isDead = true;
        
        rb.linearVelocity = Vector2.zero; 
        
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound, deathSoundVolume);
            StartCoroutine(ReloadSceneAfterDelay(deathSound.length));
        }
        else
        {
            ReloadScene();
        }
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void HandleStepSounds(float input, bool grounded)
    {
        if (grounded && Mathf.Abs(input) > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                if (walkSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(walkSound);
                }
                stepTimer = stepInterval; 
            }
        }
        else
        {
            stepTimer = 0f; 
        }
    }

    void Jump()
    {
        if (jumpSound != null && audioSource != null)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
            audioSource.PlayOneShot(jumpSound);
        }
    }
}