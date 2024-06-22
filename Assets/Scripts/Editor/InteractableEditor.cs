using Unity.VisualScripting;
using UnityEditor;

// Custom inspector for the Interactable class
[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    // Override the default inspector GUI
    public override void OnInspectorGUI()
    {
        // Cast the target object to Interactable
        Interactable interactable = (Interactable)target;

        // Check if the target is of type EventOnlyInteractable
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            // Draw a text field for the prompt message
            interactable.promptMassage = EditorGUILayout.TextField("Prompt Message", interactable.promptMassage);
            // Display a help box message
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents.", MessageType.Info);

            // Ensure InteractionEvent component is present
            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            // Draw the default inspector
            base.OnInspectorGUI();

            // Check if events are being used
            if (interactable.useEvents)
            {
                // Add InteractionEvent component if not present
                if (interactable.GetComponent<InteractionEvent>() == null)
                    interactable.gameObject.AddComponent<InteractionEvent>();
            }
            else
            {
                // Remove InteractionEvent component if present
                InteractionEvent interactionEvent = interactable.GetComponent<InteractionEvent>();
                if (interactionEvent != null)
                {
                    DestroyImmediate(interactionEvent);
                }
            }
        }
    }
}
