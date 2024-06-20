using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI promptText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
