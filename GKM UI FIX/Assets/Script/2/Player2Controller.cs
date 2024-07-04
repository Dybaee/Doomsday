using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class Player2Controller : MonoBehaviour
{
    // Variables

    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = 20f;
    public float turnSmoothTime = 0.1f;
    private float velocityThreshold = 0.1f;
    public float HP;
    public float maxHP = 500;
    public float knockbackForce = 10f;

    private float movementSpeed;
    private bool isRunning;
    private bool isJumping;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private ParticleSystem WalkFX;
    private Animator FTB;
    private Animator DiedText;

    public Slider healthSlider;
    [SerializeField] GameObject _setting;
    HpStats _hpStats;

    private Combat2Player combatPlayer;

    private bool isAlive = true;
    bool gamePaused;

    ItemOnGround item;
    public bool isFPressed = false;
    public BoxCollider attackCollider;
    public CinemachineShake vCam;
    public CameraShake camShake;

    // Knockback
    private Vector3 knockbackDirection;
    public float knockbackTimer { get; private set; }
    private float knockbackDuration = 0.5f; // Knockback Effect

    // Regen
    public float regenAmountPerSecond = 50f; // Amount of HP 
    public float regenDelay = 5f; // Delay after being hit 
    private float regenTimer = 0f;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        WalkFX = GameObject.FindGameObjectWithTag("walkfx").GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        combatPlayer = GetComponent<Combat2Player>();
        _hpStats = GetComponent<HpStats>();
        HP = maxHP;
        healthSlider.maxValue = maxHP;
        healthSlider.value = HP;
        item = GetComponent<ItemOnGround>();

        // Subscribe ke event OnHpChanged dari HpStats
        FTB = GameObject.FindGameObjectWithTag("ftb").GetComponent<Animator>();
        DiedText = GameObject.FindGameObjectWithTag("dietext").GetComponent<Animator>(); 
    }

    void Update()
    {
        if (isAlive)
        {
            if (knockbackTimer > 0)
            {
                // Apply knockback
                StartCoroutine(vCam.Shake());
                knockbackTimer -= Time.deltaTime;
                controller.Move(knockbackDirection * knockbackForce * Time.deltaTime);
            }
            else
            {
                Move();
                Jump();
                Run();

                // Check player is not attacking or not hit by enemies
                if (!combatPlayer.IsAttacking && regenTimer <= 0)
                {
                    HP += regenAmountPerSecond * Time.deltaTime;
                    HP = Mathf.Clamp(HP, 0f, maxHP); // Ensure HP stays within bounds
                    UpdatePlayerHealthUI();
                }
                else
                {
                    // Start regen
                    regenTimer -= Time.deltaTime;
                    if (regenTimer < 0)
                    {
                        regenTimer = 0;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamePaused = !gamePaused;
                Time.timeScale = 0f;
            }

            if (gamePaused)
            {
                _setting.SetActive(true);
                Time.timeScale = 0f;
                combatPlayer.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                _setting.SetActive(false);
                Time.timeScale = 1f;
                combatPlayer.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void Move()
    {
        if (!isAlive || combatPlayer.IsAttacking)
            return;

        // Get Input for movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Check if there's movement
        if (direction.magnitude >= velocityThreshold)
        {
            // Calculate angle rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Calculate movement direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * walkSpeed * (isRunning ? runSpeed : 1) * Time.deltaTime);

            // Set animation
            float speed = isRunning ? 1.5f : 0.5f; // Default to walking
            if (isRunning && direction.magnitude >= 1f)
            {
                speed = 1.5f; // Running
            }
            else if (!isRunning && direction.magnitude >= 0.5f)
            {
                speed = 0.5f; // Walking
            }
            animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);

            // Walk FX

           if (WalkFX == null) { return; }
           if (WalkFX.isStopped)
           {
                WalkFX.Play();
           }
           
        }
        else
        {
            animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime); // idle
            
            if (WalkFX == null) { return; }
            WalkFX.Stop();
        }
    }

    void Jump()
    {
        if (!isAlive)
            return;

        if (controller.isGrounded && !isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
            velocity.y = Mathf.Sqrt(2f * jumpForce * gravity); // Calculate jump velocity
        }
        else
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            velocity.y -= gravity * Time.deltaTime;
            isJumping = false;
        }
        controller.Move(velocity * Time.deltaTime); // Apply movement
    }

    void Run()
    {
        if (!isAlive)
            return;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            movementSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            movementSpeed = walkSpeed;
        }
    }

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger("Die");
        regenTimer = regenDelay; // Reset regen delay 
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        FTB.Play("FadeToBlack");
        yield return new WaitForSeconds(1);
        DiedText.Play("YouDied");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            Die();
        }

        UpdatePlayerHealthUI();
        regenTimer = regenDelay; // Reset regen delay 

    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        knockbackDirection = direction;
        knockbackForce = force;
        knockbackTimer = knockbackDuration;
    }

    void EnableAttack()
    {
        attackCollider.enabled = true;
    }

    void DisableAttack()
    {
        attackCollider.enabled = false;
    }
    void UpdatePlayerHealthUI()
    {
        healthSlider.value = HP;
    }
}
