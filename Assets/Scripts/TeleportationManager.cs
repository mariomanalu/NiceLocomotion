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

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
        };

        provider.QueueTeleportRequest(request);
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = true;
        snapTurnProvider.enabled = false;
        Debug.Log("ONTELEPORTACTIVATE");
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        _isActive = false;
        snapTurnProvider.enabled = true;
        Debug.Log("ONTELEPORTCANCEL");
    }
}
