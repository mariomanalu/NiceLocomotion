using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class TeleportationManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset actionAsset;

    private InputAction _thumbstick;

    [SerializeField]
    private XRRayInteractor rayInteractor;

    [SerializeField]
    TeleportationProvider provider;

    [SerializeField]
    ActionBasedSnapTurnProvider snapTurnProvider;

    bool _isActive;
    // bool toggleVal;
    // UnityEngine.XR.InputDevice controller;
    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        var cancel = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        _thumbstick = actionAsset.FindActionMap("XRI LeftHand").FindAction("Move");
        _thumbstick.Enable();

        // var inputDevices = new List<UnityEngine.XR.InputDevice>();
        // UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        // controller = inputDevices[1]; // 1 represents left hand
        

    }

    // Update is called once per frame
    void Update()
    { 

        if (!_isActive)
        {
            return;
        }

        if (_thumbstick.triggered)
            return;

        teleportDestination destination = CheckLocation();
        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = destination.location,
        };

        if (destination.validDestination)
        {
            Debug.Log("REQUESTED POSITION " + request.destinationPosition);
            provider.QueueTeleportRequest(request);
        }
        else
        {
            rayInteractor.enabled = false;
            return;
        }
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = true;
        // snapTurnProvider.enabled = false;
        Debug.Log("ONTELEPORTACTIVATE");
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        _isActive = false;
        // snapTurnProvider.enabled = true;
    }

    struct teleportDestination
    {
        public Vector3 location;
        public bool validDestination;
  
    }

    private teleportDestination CheckLocation()
    {
        teleportDestination destination = new teleportDestination();
        destination.validDestination = false;
        
        if (!(rayInteractor.enabled))
        {
  
            return destination;
        }
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        { 
            return destination;
        }

        Debug.Log("HIT POINT" + hit.point);
       if (hit.transform.GetComponent<TeleportationArea>())
        {
            destination.validDestination = true;
            destination.location = hit.point;
            Debug.Log(destination.location);
        }
        else
        {
            destination.validDestination = false;
            return destination;
        }

        return destination;
    }
}
