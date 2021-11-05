using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PullManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;

    private InputAction _thumbstick;

    [SerializeField] private XRRayInteractor rayInteractor; // right ray interactor
    public Transform attachAnchorTransform;
    protected InputAction grab;

    bool _isActive;
    float pullSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI RightHand").FindAction("Alternate Force Pull Activate");
        activate.Enable();
        activate.performed += OnForcePullActivate;

        grab = actionAsset.FindActionMap("XRI RightHand").FindAction("Select");

        _thumbstick = actionAsset.FindActionMap("XRI RightHand").FindAction("Move");
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

        grabbableObjects item = CheckGrabbable();

        if(item.grabbable && grab.phase != InputActionPhase.Waiting)
        {
            item.rigidbody.MovePosition(Vector3.MoveTowards(
                item.rigidbody.position, attachAnchorTransform.position, pullSpeed * Time.deltaTime));
        }
       
        
        rayInteractor.enabled = false;
        return;
        

    }

    private void OnForcePullActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = true;
    }

    struct grabbableObjects
    {
        public bool grabbable;
        public Rigidbody rigidbody;
    }

    // I don't this I would recommend going about this this way. I might do something closer to the code i had for the highlighting demo
    // There I have an object that is marked as highlightable and the controller keeps track of the object it is currently hightlighting
    // That way we can have it so that once the object has been pointed to and grabbed, we can let go of the joystick and move it around with our hand
    private grabbableObjects CheckGrabbable()
    {
       grabbableObjects item = new grabbableObjects();
       item.grabbable = false;

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            return item;
        }
        else if(hit.transform.GetComponent<XRGrabInteractable>())
        {
            item.grabbable = true;
            item.rigidbody = hit.transform.GetComponent<Rigidbody>();
            return item;
        }

        return item;
    }

}
