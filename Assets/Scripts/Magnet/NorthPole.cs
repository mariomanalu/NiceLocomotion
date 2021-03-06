using UnityEngine;

public class NorthPole : MonoBehaviour
{
    // [SerializeField, Min(1)]
    // int sideLength = 1;
    [SerializeField, Min(1)]
    int xLength = 1, yLength = 1, zLength = 1;
    [SerializeField]
    float spacing = 1;


    Vector3 originPosition, centerPosition;

    ComputeBuffer positionsBuffer,
        bFieldBuffer,
        vectorsBuffer,  // Should we instead use registers to globally bind these extra buffers?
        plotVectorsBuffer, 
        vector2Buffer,
        vector3Buffer;

    [SerializeField]
    ComputeShader computeShader;

    static readonly int
        //coulombID = Shader.PropertyToID("_CoulombConstant"),
        spacingID = Shader.PropertyToID("_Spacing"),
        originID = Shader.PropertyToID("_OriginPosition"),
        centerID = Shader.PropertyToID("_CenterPosition"),
        // sideLengthID = Shader.PropertyToID("_SideLength"),
        xLengthID = Shader.PropertyToID("_XLength"),
        yLengthID = Shader.PropertyToID("_YLength"),
        zLengthID = Shader.PropertyToID("_ZLength"),
        positionsBufferID = Shader.PropertyToID("_Positions"),
        vectorBufferID = Shader.PropertyToID("_Vectors"),
        plotVectorsBufferID = Shader.PropertyToID("_PlotVectors"),
        vector2BufferID = Shader.PropertyToID("_Vectors2"),
        vector3BufferID = Shader.PropertyToID("_Vectors3"),
        bFieldID = Shader.PropertyToID("_BField"),
        chargePositionID = Shader.PropertyToID("_ChargePosition"),
        chargeID = Shader.PropertyToID("_Charge");
    // Include the properties of the shader that we need to be able to update here. 

    [SerializeField]
    Material material;
    [SerializeField]
    Mesh mesh;
    [SerializeField]
    Transform prefab;
    Vector3 north_pole_coordinate;
    public GameObject pole;

    [SerializeField]
    int charge;
    private void OnEnable()
    {
        // sideLength = 2 * size + 1;
        originPosition = transform.position;
        centerPosition = originPosition + new Vector3(xLength - 1, yLength - 1, zLength - 1) * 0.5f * spacing;
        Vector3 translation = new Vector3(0,0,-0.5f);
        north_pole_coordinate = pole.GetComponent<Transform>().position + translation;
        int volume = xLength * yLength * zLength;

        unsafe // This could maybe be a source of problems.
        {
            positionsBuffer = new ComputeBuffer(volume, sizeof(Vector3));
            vectorsBuffer = new ComputeBuffer(volume, sizeof(Vector3)); // last arg: size of single object
            plotVectorsBuffer = new ComputeBuffer(volume, sizeof(Vector3));
            bFieldBuffer = new ComputeBuffer(volume, sizeof(Vector3));
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

        vectorsBuffer.Release();
        vectorsBuffer = null;

        plotVectorsBuffer.Release();
        plotVectorsBuffer = null;

        vector2Buffer.Release();
        vector2Buffer = null;

        vector3Buffer.Release();
        vector3Buffer = null;
    }



    // Update is called once per frame
    void Update()
    {
        UpdateGPU();
    }



    void UpdateGPU()
    {
        // The data is sent to the computeShader for calculation %%%%%%%%%
        // computeShader.SetInt(sideLengthID, sideLength);
        computeShader.SetFloat(spacingID, spacing);
        computeShader.SetVector(originID, originPosition);
        computeShader.SetVector(centerID, centerPosition);

        computeShader.SetInt(xLengthID, xLength);
        computeShader.SetInt(yLengthID, yLength);
        computeShader.SetInt(zLengthID, zLength);

        computeShader.SetInt(chargeID, charge);

        computeShader.SetBuffer(0, positionsBufferID, positionsBuffer);
        computeShader.SetBuffer(0, vectorBufferID, vectorsBuffer);
        computeShader.SetBuffer(0, plotVectorsBufferID, plotVectorsBuffer);
        computeShader.SetBuffer(0, vector2BufferID, vector2Buffer);
        computeShader.SetBuffer(0, vector3BufferID, vector3Buffer);
        computeShader.SetBuffer(0, bFieldID, bFieldBuffer);
        computeShader.SetBuffer(0, positionsBufferID, positionsBuffer);
        // Why does this need to be redone every frame?

        // This does the math and stores information in the positionsBuffer. %%%%%%%%%
        // int numGroups = Mathf.CeilToInt(sideLength / 4f); // Why this?
        int XGroups = Mathf.CeilToInt(xLength / 4f);
        int YGroups = Mathf.CeilToInt(yLength / 4f);
        int ZGroups = Mathf.CeilToInt(zLength / 4f);
        computeShader.Dispatch(0, XGroups, YGroups, ZGroups);

        // Then the data from the computeShader is sent to the shader to be rendered. %%%%%%%%
        material.SetBuffer(positionsBufferID, positionsBuffer);
        material.SetBuffer(vectorBufferID, vectorsBuffer);
        material.SetBuffer(plotVectorsBufferID, plotVectorsBuffer);
        material.SetBuffer(vector2BufferID, vector2Buffer);
        material.SetBuffer(vector3BufferID, vector3Buffer);
        material.SetBuffer(bFieldID, bFieldBuffer);

        // Here should be information about bounds and a call to draw...
        var bounds = new Bounds(centerPosition, 
            2 * centerPosition - originPosition);
        // This boundary needs revision
        Graphics.DrawMeshInstancedProcedural(mesh, 0, material, bounds, xLength * yLength * zLength);
    }
}
