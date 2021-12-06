using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmpereText : MonoBehaviour
{
   [SerializeField]
    TextMeshProUGUI display;

    [SerializeField]
    ControllerButton controller;
    
    void Update(){
        int pageNumber = controller.pageNumber % 5;
        if (pageNumber < 0)
        {
            pageNumber += 5;
        }
        switch(pageNumber)
        {
            case 0:
                display.SetText($"André-Marie Ampère (1775-1836).");
                break;
            case 1:
                display.SetText($"He quickly reacted to Hans Christian Oersted’s discovery of the connection between electricity and magnetism.");
                break;
            case 2:
                display.SetText($"As a result, he made some essential discoveries and derived many useful equations.");
                break;
            case 3:
                display.SetText($"He discovered that a hollow coil of current-carrying wire behaves like a cylindrical bar magnet with the same surface area.");
                break;
            case 4:
                display.SetText($"He also derived the first equations that describe the repulsion and attraction between two current carrying wires.");
                break;
        }
    }
}
