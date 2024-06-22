using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This class handles the player's UI, specifically for updating text prompts.
public class PlayerUi : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component used to display the prompt text.
    [SerializeField] private TextMeshProUGUI promptText;

    // This function is called when the script instance is being loaded.
    // Currently, it's not doing anything, so it can be safely removed or utilized for initialization if needed.
    void Start()
    {
        // Initialization code can go here if needed in the future.
    }

    // Updates the UI text with the provided prompt message.
    public void UpdateText(string promptMessage)
    {
        // Check if the promptText component is assigned to avoid potential null reference exceptions.
        if (promptText != null)
        {
            promptText.text = promptMessage;
        }
    }
}
