using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    [SerializeField] GameObject room;
    private Vector3 nextOffset;
    private Vector3 prevOffset;
    private Vector3 initPos;
    private Vector3 currentPos;
    private Vector3 goalPos;

    private bool next;
    private bool prev;
    private void Start()
    {
        nextOffset = new Vector3(-18.6f, 0, 0);
        prevOffset = new Vector3(18.6f, 0, 0);
    }

    private void Update()
    {
        currentPos = room.transform.position;

        if (next)
        {
            room.transform.position = Vector3.Lerp(room.transform.position, goalPos, 5f * Time.deltaTime);
        }

        if (prev)
        {
            room.transform.position = Vector3.Lerp(room.transform.position, goalPos, 5f * Time.deltaTime); 
        }

        if (Vector3.Distance(room.transform.position, goalPos) < 0.1f)
        {
            next = false;
            prev = false;
            Debug.Log("STOPPING");
        }
    }
    public void NextRoom()
    {
        next = true;
        goalPos = room.transform.position + nextOffset;
    }

    public void PrevRoom()
    {
        prev = true;
        goalPos = room.transform.position + prevOffset;
    }
}
