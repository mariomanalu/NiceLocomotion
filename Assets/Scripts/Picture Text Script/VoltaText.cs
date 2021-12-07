using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VoltaText : MonoBehaviour
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
            // Courtesy of wikipedia: https://en.wikipedia.org/wiki/Alessandro_Volta
            case 0:
                display.SetText($"Alessandro Volta (1745 - 1827).");
                break;
            case 1:
                display.SetText($"He was an Italian physicist, chemist, and pioneer of electricity and power.");
                break;
            case 2:
                display.SetText($"He is credited as the inventor of electric battery.");
                break;
            case 3:
                display.SetText($"He invented the voltaic pile in 1799.");
                break;
            case 4:
                display.SetText($"With this invention, he proved that electricity could be generated chemically.");
                break;
            case 5:
                display.SetText($"He disproved the prevalent theory that electricity was generated solely by living beings.");
                break;
            case 6:
                display.SetText($"The SI unit of electric potential is named in his honour as the volt.");
                break;
        }
    }
}
