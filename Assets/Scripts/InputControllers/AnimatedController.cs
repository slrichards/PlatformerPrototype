using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatedController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputController inputController;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    private float horizontalInput;
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 10f;
    private bool isGrounded = false;
    private bool shouldJump = false;
    [Header("Player Settings")]
    [SerializeField] private float playerHealth = 10f;

    [Header("Boomerang Settings")]
    [SerializeField] private GameObject boomerangPrefab;
    [SerializeField] private Transform playerTransform;
    private bool previousThrowState = false;
    private GameObject boomerangObject = null;

    private int kills = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputController = PlayerInputController.Instance;
    }
    private void Start()
    {
        inputController = PlayerInputController.Instance;
    }
    private void Update()
    {
        //checks x axis movement
        horizontalInput = inputController.MoveInput.x;
        shouldJump = inputController.JumpTriggered && isGrounded;

        //check jumping
        if (horizontalInput != 0)
        {
            FlipSprite(horizontalInput);
        }

        if(inputController.ThrowTriggered && !previousThrowState && boomerangObject == null)
        {
            Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;

            boomerangObject = Instantiate(boomerangPrefab, playerTransform.position, Quaternion.identity);
            Boomerang boomerang = boomerangObject.GetComponent<Boomerang>();
            boomerang.Initialize(mouseWorldPos, playerTransform);
            if (boomerang == null)
            {
                boomerangObject = null;
            }
        }
        previousThrowState = inputController.ThrowTriggered;

        if(playerHealth <= 0)
        {
            Object.Destroy(this);
        }
    }
    private void FixedUpdate()
    {
        //Apply jumping
        ApplyMovement();
        if (shouldJump == true)
        {
            ApplyJump();
        }
    }
    void ApplyMovement()
    {
        float speed = moveSpeed * (inputController.SprintValue > 0 ? sprintMultiplier : 1f);
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }
    void ApplyJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Player is on the ground!");
        }
    }

    private void FlipSprite(float horizontalMovement)
    {
        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void DoDamage(float damage)
    {
        this.playerHealth = this.playerHealth - damage;
    }
    public void IncreaseKills() { this.kills++; }
}
