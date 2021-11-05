using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabPointer : MonoBehaviour
{

    public Transform attachAnchorTransform;
    public Gradient visibleColorGradient;  
    public Gradient invisibleColorGradient;

    // Store the object that the user is or just pointed at
    Highlightable pointingAt;
    bool holding;
    Vector3 offsetRotation;

    UnityEngine.XR.InputDevice controller;

    [SerializeField] XRInteractorLineVisual rayInteractor;

    float pullSpeed = 12f;
    float dist;

    void Start(){
        rayInteractor = GetComponent<XRInteractorLineVisual>();
        rayInteractor.validColorGradient = invisibleColorGradient;
        rayInteractor.invalidColorGradient = invisibleColorGradient;
        holding = false;
        dist = 1;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        try{
            controller = inputDevices[2]; // 2 represents right hand
        } catch(ArgumentOutOfRangeException){
        }
        
        

        if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out float grip) && grip > 0.1f)
        {
            rayInteractor.validColorGradient = visibleColorGradient;
            rayInteractor.invalidColorGradient = visibleColorGradient;
            Debug.Log("Grip down");
            if(controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out bool gripButton) && gripButton){
                if(pointingAt){
                    
                    if(!holding) dist = Vector3.Magnitude(pointingAt.transform.position - transform.position);
                    holding = true;
                    
                    if(Vector3.Magnitude(pointingAt.transform.position - transform.position) > 0.1f){
                        pointingAt.gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(
                            pointingAt.gameObject.GetComponent<Rigidbody>().position, attachAnchorTransform.position, dist * pullSpeed * Time.deltaTime));
                    } else{
                        pointingAt.transform.rotation = transform.rotation;
                        pointingAt.transform.position = transform.position;
                        rayInteractor.validColorGradient = invisibleColorGradient;
                        rayInteractor.invalidColorGradient = invisibleColorGradient;
                        pointingAt.Unhighlight();
                        
                    }
                }
            }
        } else{
            rayInteractor.validColorGradient = invisibleColorGradient;
            rayInteractor.invalidColorGradient = invisibleColorGradient;
            holding = false;
        }

        // Send out a raycast from position in forward direction. Store hit info in RaycastHit object, travel 500 units
        if(!holding && Physics.Raycast(transform.position, transform.forward, out hit, 500)){

            // Checks that the hit object is the correct type
            if(hit.collider.tag == "Grabbable"){
                // Incase we switch from previous object to a new object in one frame
                if(pointingAt && hit.collider.GetComponent<Highlightable>() != pointingAt){
                    pointingAt.Unhighlight();
                }

                // Update the pointingAt var and highlight it
                pointingAt = hit.collider.GetComponent<Highlightable>();
                pointingAt.Highlight();
            } else{
                // If the object we hit is not highlightable, unhighlight the last object we highlighted
                if(pointingAt){
                    pointingAt.Unhighlight();
                    pointingAt = null;
                }
            }
        } else{
            // If no object is hit, unhighlight the last object we highlighted
            if(!holding && pointingAt){
                pointingAt.Unhighlight();
                pointingAt = null;
            }
        }
    }
}
