using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMagnet : MonoBehaviour
{
    
    public float sliderValue;
    
    public bool isPaused;
    
    [SerializeField]
    GameObject magnet;

    private Vector3 magnetPosition;
    void Start(){
        sliderValue = 25.1619f;
        isPaused = true;

        magnetPosition = magnet.transform.position;
    }
    // Update is called once per frame
    void Update()
    {  
        // If NOT paused
        if (!isPaused)
        {
            transform.position = new Vector3(2 * Mathf.Cos(Mathf.PI  * sliderValue * .1f) - .138f, magnetPosition.y, magnetPosition.z);
        }
    }
}
