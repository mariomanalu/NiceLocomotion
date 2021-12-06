using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PresidencyText : MonoBehaviour
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
                display.SetText($"Three leaders and Faraday.");
                break;
            case 1:
                display.SetText($"Faraday was offered the presidency of the United Kingdom’s national academy of sciences, called the Royal Society.");
                break;
            case 2:
                display.SetText($"He declined on both occassions.");
                break;
            case 3:
                display.SetText($"Many claim that he was also offered a knighthood, but that he declined this honor as well.");
                break;
            case 4:
                display.SetText($"Pictured is an illustration of three leaders of the Royal society offering Faraday the presidency.");
                break;
        }
    }
}
