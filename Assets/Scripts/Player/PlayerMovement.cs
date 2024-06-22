using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController; // Reference to the CharacterController component
    private Vector3 playerVelocity; // Stores the player's current velocity
    private bool isGrounded; // Checks if the player is on the ground
    private bool sprinting; // Checks if the player is sprinting
    private bool crouching; // Checks if the player is crouching
    private bool lerpCrouch; // Indicates if the player is transitioning between crouching and standing
    private float crouchTimer = 2f; // Timer for crouch transition
    [SerializeField] float speed = 5f; // Player's normal movement speed
    [SerializeField] float gravity = -9.8f; // Gravity affecting the player
    [SerializeField] float jumpHeight = 3f; // Player's jump height

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Initialize the CharacterController
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded; // Update isGrounded status

        HandleCrouchTransition(); // Handle crouch/stand transition
    }

    // Handle crouch/stand transition with lerp
    private void HandleCrouchTransition()
    {
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p; // Smoothing factor

            if (crouching)
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            else
                characterController.height = Mathf.Lerp(characterController.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    // Process movement based on input
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y); // Create movement direction vector
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); // Move the player
        ApplyGravity(); // Apply gravity
    }

    // Apply gravity to the player
    private void ApplyGravity()
    {
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        characterController.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    // Make the player jump
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    // Toggle crouch state
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    // Toggle sprint state and adjust speed
    public void Sprint()
    {
        sprinting = !sprinting;
        speed = sprinting ? 8 : 5;
    }
}
