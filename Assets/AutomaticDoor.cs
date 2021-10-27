using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool moving;
    bool closing;
    private Vector3 doorPos;
    private Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        closing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Debug.Log("MOVING");
            // door.transform.position = Vector3.Lerp(doorPos, endPos, 0.1f * Time.deltaTime);
            door.transform.position = endPos;
        }
        if (closing)
        {
            Debug.Log("CLOSING:");
            // door.transform.position = Vector3.Lerp(doorPos, endPos, 0.1f * Time.deltaTime);
            door.transform.position = endPos;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Cube")
        {
            moving = true;
            closing = false;
            doorPos = collider.gameObject.transform.position;
            endPos = new Vector3(doorPos.x, doorPos.y, doorPos.z + 2);

            
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Cube")
        {
            moving = false;
            closing = true;
            doorPos = collider.gameObject.transform.position;
            endPos = new Vector3(doorPos.x, doorPos.y, doorPos.z - 2);
           
        }
    }
}
