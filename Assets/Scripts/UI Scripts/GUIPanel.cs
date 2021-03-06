using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GUIPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;
    [SerializeField]
    VectorField field;
    [SerializeField]
    GameObject magnet;
    [SerializeField]
    Slider slider;

    private Vector3 magnetPosition;

    void Start()
    {
        magnetPosition = magnet.transform.position;
    }
    public void PlayMagnet()
    {   
        if (magnet.GetComponent<MoveMagnet>().isPaused){
            magnet.GetComponent<MoveMagnet>().isPaused = false;
        }
    }

    public void PauseMagnet()
    {
        if(!magnet.GetComponent<MoveMagnet>().isPaused)
        {   
             magnet.GetComponent<MoveMagnet>().isPaused = true;
        }
    }

    public void SlideMagnet()
    {
        if(magnet.GetComponent<MoveMagnet>().isPaused){ 
            magnet.transform.position = new Vector3(slider.value, magnetPosition.y, magnetPosition.z);
            magnet.GetComponent<MoveMagnet>().sliderValue = 10 * (Mathf.Acos((slider.value + 0.138f)/2)) / Mathf.PI;
        }   
    }

    void Update(){
        magnetPosition = magnet.transform.position;
        // Max of magnet.transform.position.x = 2 * max(Mathf.Cos(Mathf.PI * slider.value * .1f) - .138f) = 1.862
        // Min of magnet.transform.position.x = 2 * min(Mathf.Cos(Mathf.PI * slider.value * .1f) - .138f) = -2.138
        // Refactor magnet.transform.position.x later such that we don't need any offset
        if (!magnet.GetComponent<MoveMagnet>().isPaused)
        {   
            magnet.GetComponent<MoveMagnet>().sliderValue += 0.05f;
            slider.value = magnet.transform.position.x;
        }
        
        int identifier = (int)(field.fieldType);

        switch(identifier)
        {
            case 0:
                display.SetText($"Showing the Magnetic Field generated by the magnet.");
                break;
            case 1:
                display.SetText($"Showing the Electric Field generated by the magnet.");
                break;
        }
        
    }
}