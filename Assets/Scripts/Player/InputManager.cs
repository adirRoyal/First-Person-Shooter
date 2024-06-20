using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFootActions;

    private PlayerMovement movement;
    private PlayerLook look;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        onFootActions.Jump.performed += ctx => movement.Jump();

        onFootActions.Crouch.performed += ctx => movement.Crouch();
        onFootActions.Sprint.performed += ctx => movement.Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermovement to move using the value from our movement action.
        movement.ProcessMove(onFootActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFootActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFootActions.Enable(); 
    }

    private void OnDisable()
    {
        onFootActions.Disable();
    }
}
