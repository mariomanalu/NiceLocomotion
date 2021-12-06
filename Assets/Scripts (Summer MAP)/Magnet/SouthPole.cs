using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthPole : MonoBehaviour
{   
    [SerializeField]
    int charge;
    public GameObject pole;
    private static int numberOfIndices = 100;
    private static int sizeOfItem = 3*4;

    private int size = numberOfIndices * sizeOfItem;
    ComputeBuffer positionsBuffer = new ComputeBuffer(numberOfIndices, sizeOfItem);
    ComputeBuffer bFieldBuffer,
    vector2Buffer,
    vector3Buffer;

    [SerializeField]
    ComputeShader computeShader;
    
    int kernelNumber = 0;
    static int xLength = 1, yLength = 1, zLength = 1;

    static readonly int positionsBufferID = Shader.PropertyToID("_Positions"),
    bFieldID = Shader.PropertyToID("_BField"),
    xLengthID = Shader.PropertyToID("_XLength"),
    yLengthID = Shader.PropertyToID("_YLength"),
    zLengthID = Shader.PropertyToID("_ZLength"),
    chargePositionID = Shader.PropertyToID("_ChargePosition"),
    vector2BufferID = Shader.PropertyToID("_Vectors2"), 

    vector3BufferID = Shader.PropertyToID("_Vectors3"), 
    chargeID = Shader.PropertyToID("_Charge");

    [SerializeField]
    Material material;
    [SerializeField]
    Mesh mesh;

    Vector3 south_pole_coordinate;
    Vector3 center = new Vector3(0,0,0);
    Vector3 boundSize = new Vector3(5,5,5);

    
     private void OnEnable()
    {
        Vector3 translation = new Vector3(0,0,0.5f);
        south_pole_coordinate = pole.GetComponent<Transform>().position + translation;
        int volume = xLength * yLength * zLength;

        unsafe // This could maybe be a source of problems.
        {
            positionsBuffer = new ComputeBuffer(volume, sizeof(Vector3));
            bFieldBuffer = new ComputeBuffer(volume, sizeof(float));
            vector2Buffer = new ComputeBuffer(volume, sizeof(Vector3));
            vector3Buffer = new ComputeBuffer(volume, sizeof(Vector3));
        }
    }
    private void OnDisable()
    {
        positionsBuffer.Release();
        positionsBuffer = null;

        bFieldBuffer.Release();
        bFieldBuffer = null;
    }

    void Update(){
        UpdateGPU();
    }
    
    void UpdateGPU(){
        computeShader.SetInt(xLengthID, xLength);
        computeShader.SetInt(yLengthID, yLength);
        computeShader.SetInt(zLengthID, zLength);
        computeShader.SetInt(chargeID, charge);
        computeShader.SetVector(chargePositionID, south_pole_coordinate);
        computeShader.SetBuffer(kernelNumber, bFieldID, bFieldBuffer);
        computeShader.SetBuffer(kernelNumber, positionsBufferID, positionsBuffer);

    
        int XGroups = Mathf.CeilToInt(xLength / 4f);
        int YGroups = Mathf.CeilToInt(yLength / 4f);
        int ZGroups = Mathf.CeilToInt(zLength / 4f);
        computeShader.Dispatch(0, XGroups, YGroups, ZGroups);
        // Where is the result of the computation computed to?
        //positionsBuffer.GetData(outputArray); 

        material.SetBuffer(bFieldID, bFieldBuffer);
        var bounds = new Bounds(center, boundSize);
        Graphics.DrawMeshInstancedProcedural(mesh, 0, material, bounds, xLength*yLength*zLength);
    }
}
