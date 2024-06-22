using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Input system for player actions
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFootActions;

    // References to other components for movement and looking
    private PlayerMovement movement;
    private PlayerLook look;

    // Called when the script instance is being loaded
    void Awake()
    {
        // Initialize the player input and action maps
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;

        // Get the PlayerMovement and PlayerLook components
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        // Bind input actions to corresponding methods
        BindInputActions();
    }

    // Bind input actions to corresponding methods
    private void BindInputActions()
    {
        onFootActions.Jump.performed += ctx => movement.Jump();
        onFootActions.Crouch.performed += ctx => movement.Crouch();
        onFootActions.Sprint.performed += ctx => movement.Sprint();
    }

    // Called at a fixed interval, used for physics calculations
    void FixedUpdate()
    {
        // Tell the PlayerMovement to move using the value from the movement action
        movement.ProcessMove(onFootActions.Movement.ReadValue<Vector2>());
    }

    // Called after all Update functions have been called
    private void LateUpdate()
    {
        // Tell the PlayerLook to look using the value from the look action
        look.ProcessLook(onFootActions.Look.ReadValue<Vector2>());
    }

    // Called when the object becomes enabled and active
    private void OnEnable()
    {
        onFootActions.Enable();
    }

    // Called when the object becomes disabled or inactive
    private void OnDisable()
    {
        onFootActions.Disable();
    }
}
