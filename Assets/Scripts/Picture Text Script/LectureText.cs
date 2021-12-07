using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LectureText : MonoBehaviour
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
                display.SetText($"Michael Faraday's Lecture.");
                break;
            case 1:
                display.SetText($"Lecturing was an essential component of Faraday’s job.");
                break;
            case 2:
                display.SetText($"He was very perceptive of his audiences and theatrical in his demonstrations.");
                break;
            case 3:
                display.SetText($"Faraday’s lectures were highly attended, and he presented his work to an upper-class and largely popular audience.");
                break;
        }
    }
}
