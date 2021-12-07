using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OerstedText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField]
    ControllerButton controller;
    
    void Update(){
        int pageNumber = controller.pageNumber % 6;
        if (pageNumber < 0)
        {
            pageNumber += 6;
        }
        switch(pageNumber)
        {
            case 0:
                display.SetText($"Hans Christian Oersted (1777-1851).");
                break;
            case 1:
                display.SetText($"He is credited with discovering the connection between electricity and magnetism.");
                break;
            case 2:
                display.SetText($"He investigated this relationship in a famous series of experiments published in 1819 and 1820.");
                break;
            case 3:
                display.SetText($"Oersted discovered that current carrying wires, bar magnets, and compasses all exert force on each other.");
                break;
            case 4:
                display.SetText($"His discoveries generated a frenzy of research in the following years.");
                break;
            case 5:
                display.SetText($"As a result, there was a massive increase in the number of publications on the subject - most notably those of Faraday and Ampère.");
                break;
        }
    }
}
