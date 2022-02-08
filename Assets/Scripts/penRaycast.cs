using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class penRaycast : MonoBehaviour
{
    public GameObject redPen;
    public float distance = 10.0f;
    public Material material;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray targetRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * distance, material.color);

        if(Physics.Raycast(targetRay, out hit, distance))
        {
            if(hit.collider.tag == "graph")
            {
                Debug.Log(hit.collider.name);
                hit.collider.gameObject.GetComponent<Renderer>().material = material;
            }
        }

    }
}
