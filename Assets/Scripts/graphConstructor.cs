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

    [SerializeField]
    GameObject nodePrefab;


    UnityEngine.XR.InputDevice controller;

    private bool primaryButtonPressed;
    private bool secondaryButtonPressed;

    private bool primaryButton;
    private bool secondaryButton;

    private float distance = 3.0f;

    // Create an array to store the node. For now, set the size to 3.
    private static int size = 10;
    private GameObject[] nodeArray = new GameObject[size];
    private int nodeCounter = 0;

    // Create an array to connect two nodes
    private GameObject[] connectNodeArray = new GameObject[2];
    private int connectNodeCounter = 0;

    float width = 0.2f;
    // Update is called once per frame
    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        try
        {
            controller = inputDevices[1]; // 1 represents left hand
        }
        catch (ArgumentOutOfRangeException)
        {
        }

        if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButton) && primaryButton)
        {
            if (!primaryButtonPressed)
            { 
                // buttonPressed boolean is used to catch the down event so that this code is not called every frame the button is pressed
                primaryButtonPressed = true;
            }
        }
        else
        {
            if (primaryButtonPressed)
            {
                DrawNode();
                primaryButtonPressed = false; // When the button press is let go then this is set back to false so that it can catch the first click again
            }
        }

        RaycastHit hit;
        Ray targetRay = new Ray(transform.position, transform.forward);
        Physics.Raycast(targetRay, out hit, distance);
       

        if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButton) && secondaryButton)
        {
            if (!secondaryButtonPressed)
            {
                // buttonPressed boolean is used to catch the down event so that this code is not called every frame the button is pressed
                secondaryButtonPressed = true;
            }
        }
        else
        {
            if (secondaryButtonPressed && hit.collider.tag == "Node")
            {
                if (connectNodeCounter == 0)
                {  
                    connectNodeArray[0] = hit.collider.gameObject;

                    connectNodeCounter += 1;                   
                }

                if (connectNodeCounter == 1)
                {
                    if (hit.collider.gameObject != connectNodeArray[0])
                    {
                        connectNodeArray[1] = hit.collider.gameObject;
                        Debug.Log("Calling connect node");
                        ConnectNode();
                    }
                }
                
                secondaryButtonPressed = false; // When the button press is let go then this is set back to false so that it can catch the first click again
            }
        }
    }

    void DrawNode()
    {
        // Check if the node array is full
        if (nodeCounter < size)
        {
            GameObject newNode = Instantiate(nodePrefab, cameraTransform.position + (cameraTransform.forward * 5), Quaternion.identity);
            newNode.tag = "Node";
            // newNode.AddComponent<Outline>();
            nodeArray[nodeCounter] = newNode;
            nodeCounter += 1;
        }
    }

    void ConnectNode()
    {
        Vector3 end = connectNodeArray[0].transform.position;
        Vector3 start = connectNodeArray[1].transform.position;

        Debug.Log("End position: " + end.x + end.y + end.z);
        Debug.Log("Start position: " + start.x + start.y + start.z);

        var offset = end - start;
        var scale = new Vector3(width, offset.magnitude / 2.0f, width);
        var position = start + (offset / 2.0f);

        var cylinder = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), position, Quaternion.identity);
        cylinder.transform.up = offset;
        cylinder.transform.localScale = scale;

        // connectNodeArray[0].GetComponent<Outline>().enabled = false;
        // connectNodeArray[1].GetComponent<Outline>().enabled = false;



        connectNodeArray[0] = null;
        connectNodeArray[1] = null;
        connectNodeCounter = 0;

    }
}
