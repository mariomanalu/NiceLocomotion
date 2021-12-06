using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoulombText : MonoBehaviour
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
                display.SetText($"Charles-Augustin de Coulomb (1736-1806).");
                break;
            case 1:
                display.SetText($"He discovered that the inverse-square law also applies to point charges.");
                break;
            case 2:
                display.SetText($"The inverse-square law is as follows:");
                break;
            case 3:
                display.SetText($"the strength of a force exerted on one object by another is inversely proportional to the square of the distance between the objects.");
                break;
            case 4:
                display.SetText($"Coulomb carried out an experiment with a charged needle and a charged pith ball.");
                break;
            case 5:
                display.SetText($"He measured the force exerted on the needle at varying distances.");
                break;
            case 6:
                display.SetText($"He established that Newton’s ideas about force could be applied in electricity and magnetism.");
                break;
            
        }
    }
}
