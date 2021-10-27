using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Vector3 endPosLeft;
    private Vector3 endPosRight;
    private Vector3 endPos;
    private Vector3 startPos;
    private Vector3 startPosLeft;
    private Vector3 startPosRight;
    private float delay = 0.0f;
    public float speed = 1.0f;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    // [SerializeField] Collider doorCollider;

    bool opening = true;
    bool alreadyOpen;
    bool alreadyClosed;
    bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 dummy = new Vector3(30, 0, 0);
        startPos = transform.position;
        endPos = startPos + dummy;

        Debug.Log(startPos);
        Debug.Log(endPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            if(opening)
            {
                Debug.Log("MOVING");
                MoveDoor(endPos);
            }
            else
            {
                MoveDoor(startPos);
            }
        }
    }

    void MoveDoor(Vector3 goalPos)
    {
        float dist = Vector3.Distance(transform.position, goalPos);

        if (dist > .1f)
        {
            transform.position = Vector3.Lerp(startPos, goalPos, speed * Time.deltaTime);
        }
        else
        {
            if(opening)
            {
                delay += Time.deltaTime;
                // Holding the door open for 1.5f seconds
                if (delay > 1.5f)
                {
                    opening = false;
                }
            }
            else
            {
                moving = false;
                opening = true;
            }
        }
    }

    public bool Moving
    {
        get { return moving; }
        set { moving = value; }
    }

}
