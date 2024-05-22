using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Controller : MonoBehaviour
{
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

    private float movementSpeed;
    private bool isRunning;
    private bool isJumping;
    private float turnSmoothVelocity;
    private Vector3 velocity;

    private Combat2Player combatPlayer;

    private bool isAlive = true; // Tracking player life

    ItemOnGround item;
    public bool isFPressed = false;


    private void Awake()
    {
        
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        combatPlayer = GetComponent<Combat2Player>();
        HP = maxHP;
        item = GetComponent<ItemOnGround>();
    }

    void Update()
    {
        if (isAlive)
        {
            Move();
            Jump();
            Run();
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
        }
        else
        {
            animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime); // idle
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
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
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
    }

}
