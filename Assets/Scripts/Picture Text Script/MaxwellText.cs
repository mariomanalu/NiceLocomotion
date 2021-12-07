using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MaxwellText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField]
    ControllerButton controller;
    
    void Update(){
        int pageNumber = controller.pageNumber % 4;
        if (pageNumber < 0)
        {
            pageNumber += 4;
        }
        switch(pageNumber)
        {
            case 0:
                display.SetText($"James Clerk Maxwell (1831-1879).");
                break;
            case 1:
                display.SetText($"He was an educated mathematician and physicist.");
                break;
            case 2:
                display.SetText($"He was persuaded by Faraday’s ideas and careful experimentation.");
                break;
            case 3:
                display.SetText($"He sought to derive equations which would describe Faraday’s lines of force.");
                break;
        }
    }
}
