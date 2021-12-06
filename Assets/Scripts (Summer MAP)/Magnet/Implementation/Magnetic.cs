using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    /// <summary>
    /// The extra arguments provided to the Coulomb function of the vector field. 
    /// 
    /// Index 0 is the number of charges, the other indices are the strength of the charges. 
    /// </summary>
    public ComputeBuffer floatArgs;
    /// <summary>
    /// The extra vector arguments provided to the Coulomb function of the vector field. 
    /// 
    /// Index 0 is unused, the others are positions of the charges in floatArgs
    /// </summary>
    public ComputeBuffer vectorArgs;
    
    public ComputeBuffer velocityArgs;

    /// <summary>
    /// The <cref>VectorField</cref> generating the displayed field.
    /// </summary>
    [SerializeField]
    VectorField field;

    [SerializeField]
    GameObject northPole, southPole;
    // The arrays used to initialize the argument buffers. 

    // The north pole is assumed to be positively-charged
    // Consequently, the south pole is assumed to be negatively-charged
    [NonSerialized]
    private float[] floatArray = { 2f, 3f, -3f };

    [NonSerialized]
    private Vector3[] vec_array = { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0) };

    private Vector3[] vec_array_past = { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0) };

    private Vector3[] velocity_array = {new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(0,1,0)};
    // Eventually, these will be set based on a position

    private float time;
    void Start()
    {
        // Create the ComputeBuffers
        unsafe {
            floatArgs = new ComputeBuffer(3, sizeof(float));
            vectorArgs = new ComputeBuffer(3, sizeof(Vector3));
            velocityArgs = new ComputeBuffer(3, sizeof(Vector3));
        }

        vec_array[1] = northPole.transform.position;
        vec_array[2] = southPole.transform.position;

        // Initialize the ComputeBuffers
        floatArgs.SetData(floatArray);
        vectorArgs.SetData(vec_array);
        velocityArgs.SetData(velocity_array);
    }

    void Update(){
        vec_array_past[1] = vec_array[1];
        vec_array_past[2] = vec_array[2];

        vec_array[1] = northPole.transform.position;
        vec_array[2] = southPole.transform.position;

        // VectorFields will call SetExtraArgs before every calculation.
        field.preCalculations += SetExtraArgs;

        time = Time.fixedDeltaTime;
        // Print Velocity
        velocity_array[1] = (vec_array[1] - vec_array_past[1]) / time;
        velocity_array[2] = (vec_array[2] - vec_array_past[2]) / time;
         // Initialize the ComputeBuffers
        floatArgs.SetData(floatArray);
        vectorArgs.SetData(vec_array);
        velocityArgs.SetData(velocity_array);
        
        //Debug.Log("VELOCITY IS " + velocity);
    }
    
    /// <summary>
    /// Sets the extra float and vector arguments of the <cref>VectorField</cref>
    /// </summary>
    public void SetExtraArgs()
    {   
        
        field.floatArgsBuffer = floatArgs; 
        field.vectorArgsBuffer = vectorArgs;
        field.velocityArgsBuffer = velocityArgs;
    }

    // Make sure to wipe the Compute buffers after use. Otherwise, the GPU will complain!
    private void OnDestroy()
    {
        floatArgs.Release();
        floatArgs = null;

        vectorArgs.Release();
        vectorArgs = null;

        velocityArgs.Release();
        velocityArgs = null;
    }
}