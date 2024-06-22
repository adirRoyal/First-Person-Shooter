using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam; // Reference to the player's camera
    [SerializeField] float distance = 3f; // Interaction distance
    [SerializeField] LayerMask mask; // Layer mask to specify what objects the player can interact with
    private PlayerUi playerUi; // Reference to the PlayerUi script
    private InputManager inputManager; // Reference to the InputManager script

    // Start is called before the first frame update
    void Start()
    {
        // Initialize references to other components
        cam = GetComponent<PlayerLook>().cam;
        playerUi = GetComponent<PlayerUi>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Clear the UI text every frame
        playerUi.UpdateText(string.Empty);

        // Create a ray at the center of the camera, shooting outwards
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance); // Visualize the ray in the editor

        RaycastHit hitInfo; // Variable to store collision information
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            // Check if the hit object has an Interactable component
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                // Update the UI text with the interactable's prompt message
                playerUi.UpdateText(interactable.promptMassage);

                // Check if the interact button was pressed
                if (inputManager.onFootActions.Interact.triggered)
                {
                    // Execute the interaction
                    interactable.BaseInteract();
                }
            }
        }
    }
}
