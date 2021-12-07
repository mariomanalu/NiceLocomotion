using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MagnetController : MonoBehaviour
{

    // dynamicMagnet is the dynamic magnet in the two magnets mini scene on the left of the second room
    [SerializeField]
    GameObject dynamicMagnet;

    // The slider that slides dynamicMagnet
    [SerializeField]
    Slider dynamicMagnetSlider;

    // magnetWithCoil is the dynamic magnet in the coil and magnet mini scene on the right of the second room
    [SerializeField]
    GameObject magnetWithCoil;

    // The slider that slides magnetWithCoil
    [SerializeField]
    Slider magnetWithCoilSlider;
    private float dynamicMagnetX,dynamicMagnetY,dynamicMagnetZ;
    private float magnetWithCoilX, magnetWithCoilY, magnetWithCoilZ;

    void Start()
    {
        // Get initial position
        dynamicMagnetX = dynamicMagnet.transform.position.x;
        dynamicMagnetY = dynamicMagnet.transform.position.y;
        dynamicMagnetZ = dynamicMagnet.transform.position.z;

        magnetWithCoilX = magnetWithCoil.transform.position.x;
        magnetWithCoilY = magnetWithCoil.transform.position.y;
        magnetWithCoilZ = magnetWithCoil.transform.position.z;
        
    }
    public void SlideLeftMagnet()
    {
        // Adjust position based on the slider's value
        dynamicMagnet.transform.position = new Vector3(dynamicMagnetX, dynamicMagnetY, dynamicMagnetZ + dynamicMagnetSlider.value);
        
        magnetWithCoil.transform.position = new Vector3(magnetWithCoilX, magnetWithCoilY, magnetWithCoilZ - magnetWithCoilSlider.value);
    }
}
