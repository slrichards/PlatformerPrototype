using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatedController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private PlayerInputController inputController;
    private float horizontalInput;
    private bool isGrounded = false;
    private bool shouldJump = false;
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
}
