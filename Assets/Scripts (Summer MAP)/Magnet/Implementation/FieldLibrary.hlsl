// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
// To add a new field to the Field Library, follow these steps:
//   1) Create a new function in the field library file. DO NOT change the parameters 
//      or return type. 
//   2) Add a new kernel to VectorCompute.compute by adding the line
//          #pragma kernel [your class name]Field
//      at the top of the file and add the line
//          KERNEL_NAME([your class name])
//      at the bottom of the file. 
//   3) Add the name you want displayed for your field to the `enum FieldType` list
//      in VectorFields.cs. MAKE SURE that the order of this list is the same as the
//      order of the lines at the top of VectorCompute.compute. 
// %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
float3 Magnetic(float3 position, int index)
{
    float3 vect = float3(0.0, 0.0, 0.0);
    // The first argument in _FloatArgs is the number of charges in the system
    float numCharges = _FloatArgs[0]; 
    float i;
    for (i = 1.0; i < 3.0; i++) // numCharges + 0.0; i++)
    {
        // The zeroth index of _VectorArgs is unused so that the two buffers align.
        float3 displacement = position - (_VectorArgs[i] - _CenterPosition);
        float distance = sqrt(displacement.x * displacement.x +
                displacement.y * displacement.y +
                displacement.z * displacement.z);
        vect += _FloatArgs[i] / (pow(distance, 3)) * displacement;
        
    }
   
    return vect;
};

// Change in Magnetic Field
float3 Db(float3 position, int index){
    // Get the corresponding position of the magnetic field in the past
    float3 fieldPast = _MagneticFieldPast[index];
    
    // Calculate the position of the magnetic field now
    float3 field = Magnetic(position, index);
    
    // Compute the displacement
    float3 displacement = field - fieldPast;

    // Divide by delta time
    float3 vect =  displacement / (_TimeInterval);

    // Store the new field at index of _MagneticFieldPast
    if (length(vect)>0){
        _DbArray[index] = vect;
         _MagneticFieldPast[index] = field;
    }

    return vect;
};

// Calculate the integrand for the Maxwell-Faraday Triple Integration 
float3 Integrand(float3 position, int index){
    // Calculate the DbDt field
    float3 DbDt = Db(_Positions[index] - _CenterPosition, index);
    
    // Calculate the distance
    // THIS IS THE ACTUAL SOURCE OF THE PROBLEM
    // DISTANCE IS ALMOST ALWAYS ZERO, BUT FOR ANY VECTOR IN ROW 2 AND 10, IT IS NOT ZERO
    // CHECK THIS TOMORROW
    float3 distance = position - _Positions[index] + _CenterPosition;
    
    // THIS IS TO CHECK TO SEE THAT THE TWO ROWS ARE THE ONLY ROWS THAT ARE NOT ZERO
    if (length(distance) < 0.0001){
        return float3(0.0, 0.0, 0.0);
    }
    
    // Compute the cube of distance
    float distanceCubed = pow(length(distance), 3);
    
    // Compute integrand
    float3 integrand = cross(DbDt, distance)/distanceCubed;
   
    return integrand;
};
// Every type that's added must also be present in the enum in VectorFields.cs and have a kernel in VectorCompute.compute

