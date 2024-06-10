using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public List<AttackSO> combo;
    public WeaponDamage weapon;

    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = 20f;
    public float turnSmoothTime = 0.1f;

    private bool isRunning;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;

    void Start()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponent<WeaponDamage>();
    }

    void Update()
    {
        Movement();
        Combat();
        ExitAttack();
    }

    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * walkSpeed * (isRunning ? runSpeed : 1) * Time.deltaTime);

            animator.SetBool("IsWalking", true);
            if (isRunning)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            Combat();
        }

        // Jumping Logic
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayJumpAnimation();
                velocity.y = Mathf.Sqrt(2f * jumpForce * gravity);
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void Combat()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > 0.5f && comboCounter < combo.Count)
        {
            if (Time.time - lastClickedTime >= 0.2f)
            {
                animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                animator.CrossFadeInFixedTime("Attack 1", 0, 0);
                weapon.damage = combo[comboCounter].damage;
                comboCounter++;
                lastClickedTime = Time.time;
            }
        }
        else
        {
            comboCounter = 0;
        }
    }

    void ExitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 0.1f);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }

    void PlayJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    void OnRun(InputValue value)
    {
        isRunning = value.Get<float>() == 1f;
    }
}
