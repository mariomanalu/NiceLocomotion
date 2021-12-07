using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CoilAndMagnet : MonoBehaviour
{
    // Coil and Magnet mini scene variables

    [SerializeField]
    GameObject magnet;

    [SerializeField]
    Material material;

    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField]
    ControllerButton controller;

    float topBorder, bottomBorder;

    void Update()
    {

        topBorder = magnet.transform.position.y;
        bottomBorder = magnet.transform.position.y;

        material.SetFloat("topBorder", topBorder);
        material.SetFloat("bottomBorder", bottomBorder);

        int pageNumber = controller.pageNumber % 8;
        if (pageNumber < 0)
        {
            pageNumber += 8;
        }
        switch(pageNumber)
        {
            case 0:
                display.SetText($"Michael Faraday's Magnetic Field Induction Experiment.");
                break;
            case 1:
                display.SetText($"This exhibit is grounded on Faraday’s coil and magnet experiment  1831.");
                break;
            case 2:
                display.SetText($"He discovered electromagnetic induction while conducting the coil and magnet experiment.");
                break;
            case 3:
                display.SetText($"The explanation behind electromagnetic induction is summarized by Faraday’s Law.");
                break;
            case 4:
                display.SetText($"Faraday’s Law states that a changing magnetic field creates an electric field.");
                break;
            case 5:
                display.SetText($"The key idea illustrated in this exhibit is as follows:");
                break;
            case 6:
                display.SetText($"when the magnet moves, there is a change in the magnetic field.");
                break;
            case 7:
                display.SetText($"The change in the magnetic field generates an electric field and results in a flow of current that turns the light bulb on.");
                break;
        }
    }
}
