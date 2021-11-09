using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour
{

    public Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        // Find the outline component on the game object
        outline = GetComponent<Outline>();
    }

    // Show the outline
    public void Highlight(){
        outline.enabled = true;
    }

    // Hide the outline
    public void Unhighlight(){
        outline.enabled = false;
    }
}
