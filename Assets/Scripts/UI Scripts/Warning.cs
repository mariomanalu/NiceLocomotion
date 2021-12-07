using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Warning : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // 15 minutes == 900 seconds
        if (Time.realtimeSinceStartup > 1200f){
            GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
