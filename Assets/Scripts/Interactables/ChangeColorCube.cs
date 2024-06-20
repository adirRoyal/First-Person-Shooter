using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeColorCube : Interactable
{
    MeshRenderer mesh;
    [SerializeField] Color[] colors;
    public int colorIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = Color.red;
    }

    protected override void Interact()
    { 
        colorIndex++;
        if (colorIndex > colors.Length - 1)
        {
            colorIndex = 0;
        }
        mesh.material.color = colors[colorIndex];
    }
}
