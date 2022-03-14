using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class graphConstructor : MonoBehaviour
{
    [SerializeField] 
    private InputActionAsset actionAsset;

    [SerializeField]
    Transform cameraTransform;

    UnityEngine.XR.InputDevice controller;

    private bool buttonPressed;
    private bool button;

    [SerializeField]
    GameObject nodePrefab;

    private float distance = 3.0f;


    // Update is called once per frame
    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        try
        {
            controller = inputDevices[2]; // 2 represents right hand
            Debug.Log(controller.name);
        }
        catch (ArgumentOutOfRangeException)
        {
        }

        // Ray targetRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, cameraTransform.forward * 5, Color.red);

        
        if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out button) && button)
        {
            if (!buttonPressed)
            { // JoystickDown boolean is used to catch the down event so that this code is not called every frame the button is pressed
                buttonPressed = true;
            }
        }
        else
        {
            if (buttonPressed)
            {
                Debug.Log("Instantiating one node");
                Instantiate(nodePrefab, cameraTransform.position + (cameraTransform.forward * 5), Quaternion.identity);
                buttonPressed = false; // When the joystick click is let go then this is set back to false so that it can catch the first click again
            }

        }
        
        
    }
}
