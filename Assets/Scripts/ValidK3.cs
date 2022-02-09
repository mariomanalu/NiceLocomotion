using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidK3 : MonoBehaviour
{
    public GameObject K3;
    public GameObject V1;
    public GameObject V2;
    public GameObject V3;


    // Update is called once per frame
    void Update()
    {
        if (!isDefaultMaterial(V1) && !isDefaultMaterial(V2) && !isDefaultMaterial(V3) && isValidK3())
            K3.GetComponent<Outline>().enabled = true;
        else
            K3.GetComponent<Outline>().enabled = false;

    }

    bool isDefaultMaterial(GameObject obj)
    {
        if (obj.GetComponent<Renderer>().material.color == Color.white)
        {
            return true;
        }
        else
            return false;
    }

    bool isValidK3()
    {
        Color V1Color = V1.GetComponent<Renderer>().material.color;
        Color V2Color = V2.GetComponent<Renderer>().material.color;
        Color V3Color = V3.GetComponent<Renderer>().material.color;

        if (V1Color != V2Color && V2Color != V3Color && V3Color != V1Color)
            return true;
        else
            return false;
    }
}
