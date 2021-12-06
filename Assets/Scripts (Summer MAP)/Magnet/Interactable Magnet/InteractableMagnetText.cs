using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableMagnetText : MonoBehaviour
{   
    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField]
    ControllerButton controller;

    [SerializeField]
    VectorField field;
    // Update is called once per frame
    void Update()
    {
        if ((int)field.fieldType == 0)
        {
            display.SetText($"Take a look into the Magnetic Field from the inside!");
        }
        else
        {
            display.SetText($"Play with the Electric Field and see the direction of the curl when you move forward and backward.");
        }
    }
}
