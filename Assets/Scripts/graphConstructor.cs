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

    UnityEngine.XR.InputDevice controller;

    private bool buttonPressed;

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

        // RaycastHit hit;
        // Ray targetRay = new Ray(transform.position, transform.forward);

        // if(Physics.Raycast(targetRay, out hit, distance))
        // {
        //     Debug.Log(hit.transform.position);
        // }

        if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out buttonPressed) && buttonPressed)
        {
            Debug.Log("READ");
            if (nodePrefab)
            {
                Debug.Log("nodePrefab is not null");
            }

            
            Instantiate(nodePrefab, new Vector3(0,0,0), Quaternion.identity);
        }
        
    }
}
