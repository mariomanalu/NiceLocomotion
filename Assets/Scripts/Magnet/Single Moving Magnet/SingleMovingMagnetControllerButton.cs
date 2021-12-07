using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMovingMagnetControllerButton : MonoBehaviour
{
    public int magneticFieldDescriptionPageNum = 0;
    public int electricFieldDescriptionPageNum = 0;
    
    [SerializeField]
    VectorField field;
    public void NextPage()
    {
        // If the magnetic field is presented
        if((int)field.fieldType == 0)
        {
            magneticFieldDescriptionPageNum += 1;
        }
        // Else, the electric field is presented
        else
        {
            electricFieldDescriptionPageNum += 1;
        }
    }
    public void PreviousPage()
    {
        // If the magnetic field is presented
        if((int)field.fieldType == 0)
        {
            magneticFieldDescriptionPageNum -= 1;
        }
        // Else, the electric field is presented
        else
        {
            electricFieldDescriptionPageNum -= 1;
        }
    }
}
