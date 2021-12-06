using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLightning : MonoBehaviour
{
    
    [SerializeField]
    GameObject leftLightning;

    
    [SerializeField]
    GameObject rightLightning;

    [SerializeField]
    GameObject pointLight;

    [SerializeField]
    GameObject magnet;

    private Vector3 pastPosition;
    private Vector3 presentPosition;

    void Start()
    {
        presentPosition = magnet.transform.position;
        pastPosition = presentPosition;
        leftLightning.SetActive(false);
        rightLightning.SetActive(false);
        pointLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        presentPosition = magnet.transform.position;
        if (pastPosition != presentPosition)
        {
            leftLightning.SetActive(true);
            rightLightning.SetActive(true);
            pointLight.SetActive(true);
            pastPosition = presentPosition;
        }
        else{
            leftLightning.SetActive(false);
            rightLightning.SetActive(false);
            pointLight.SetActive(false);
        }
    }
}
