using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NewtonText : MonoBehaviour
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
                display.SetText($"Sir Isaac Newton (1642-1727).");
                break;
            case 1:
                display.SetText($"He is regarded as one of the most influential scientists of all time.");
                break;
            case 2:
                display.SetText($"He imagined that force always acted between two or more objects.");
                break;
            case 3:
                display.SetText($"He also believed that force always acted in straight lines.");
                break;
            case 4:
                display.SetText($"This Newtonian conceptualization of force is often referred to as action at a distance.");
                break;
            case 5:
                display.SetText($"This concept was widely accepted until later when Faraday showed that force do not always act in straight lines.");
                break;
        }
    }

}
