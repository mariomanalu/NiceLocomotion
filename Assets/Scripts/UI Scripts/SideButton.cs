using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideButton : MonoBehaviour
{
    [SerializeField]
    VectorField singleMovingMagnetField;

    [SerializeField]
    VectorField interactableMagnetField;


    public void singleMovingMagnetFieldNext()
    {
        singleMovingMagnetField.fieldType = (VectorField.FieldType)((((int)singleMovingMagnetField.fieldType) + 1) % 2);
    }

    public void singleMovingMagnetFieldPrev()
    {
        singleMovingMagnetField.fieldType = (VectorField.FieldType)((2 + ((int)singleMovingMagnetField.fieldType) - 1) % 2);
    }

    public void interactableMagnetFieldNext()
    {
        interactableMagnetField.fieldType = (VectorField.FieldType)((((int)interactableMagnetField.fieldType) + 1) % 2);
    }

    public void interactableMagnetFieldPrev()
    {
        interactableMagnetField.fieldType = (VectorField.FieldType)((2 + ((int)interactableMagnetField.fieldType) - 1) % 2);
    }
}
