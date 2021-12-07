using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FaradayText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField]
    ControllerButton controller;
    
    void Update(){
        int pageNumber = controller.pageNumber % 7;
        if (pageNumber < 0)
        {
            pageNumber += 7;
        }
        switch(pageNumber)
        {
            case 0:
                display.SetText($"Michael Faraday (1791-1867).");
                break;
            case 1:
                display.SetText($"He is described as the world’s most brilliant experimentalist.");
                break;
            case 2:
                display.SetText($"The concepts of electricity, magnetism, and force fields are built off of his research and ideas.");
                break;
            case 3:
                display.SetText($"Without a formal education, he conceptualized force in a way that rejected the scientific dogma of the time.");
                break;
            case 4:
                display.SetText($"Many of his ideas about force and force fields appear contradictory to Newton's.");
                break;
            case 5:
                display.SetText($"He imagined electric and magnetic force completely filling space, and not acting in straight lines.");
                break;
        }
    }
}
