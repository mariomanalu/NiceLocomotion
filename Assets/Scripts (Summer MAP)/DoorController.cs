using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Vector3 endPosLeft;
    private Vector3 endPosRight;
    private Vector3 startPosLeft;
    private Vector3 startPosRight;
    private Vector3 leftOffset;
    private Vector3 rightOffset;
    [SerializeField] float speed = 1.0f;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] Collider doorCollider;
    private bool opening;
    private bool closed;
    // Start is called before the first frame update
    void Start()
    {
        startPosLeft = leftDoor.transform.position;
        startPosRight = rightDoor.transform.position;
        opening = false;
        closed = true;

        leftOffset = new Vector3(-2.73f, 0, 0);
        rightOffset = new Vector3(2.73f, 0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, startPosLeft + leftOffset, 5 * Time.deltaTime);
            rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, startPosRight + rightOffset, 5 * Time.deltaTime);
        }
        else
        {
            leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, startPosLeft, 5 * Time.deltaTime);
            rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, startPosRight, 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.tag == "Door")
        {
            opening = true;
            Debug.Log(collider.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Door")
        {
            opening = false;
            Debug.Log(collider.gameObject.name);
        }
        
    }

}
