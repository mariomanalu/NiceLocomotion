using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SingleMovingMagnetText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField]
    SingleMovingMagnetControllerButton controller;

    [SerializeField]
    VectorField field;
    private int numOfMagneticFieldDescriptionPages;

    private int numOfElectricFieldDescriptionPages;
    void Start(){
        numOfMagneticFieldDescriptionPages = 15;
        numOfElectricFieldDescriptionPages = 14; 
        
    }
    void Update(){
        // fieldType represents the type of field is presented
        // 0 represents magnetic field
        // 1 represents electric field
        int fieldType = (int)field.fieldType;
        int pageNumber;

        // If the magnetic field is presented
        if (fieldType == 0)
        {   
            pageNumber = controller.magneticFieldDescriptionPageNum % numOfMagneticFieldDescriptionPages;
            if (pageNumber < 0)
            {
                pageNumber += numOfMagneticFieldDescriptionPages;
            }

            switch(pageNumber)
            {
                case 0:
                    display.SetText($"This section provides background information on magnetic fields.");
                    break;
                case 1:
                    display.SetText($"A magnetic field is a vector field that describes the magnetic influence on moving electric charges.");
                    break;
                case 2:
                    display.SetText($"Magnetic fields may be represented by continuous lines of force.");
                    break;
                case 3:
                    display.SetText($"The lines of flux are continuous, forming closed loops.");
                    break;
                case 4:
                    display.SetText($"The forces emerge from north-seeking magnetic poles and enter south-seeking magnetic poles.");
                    break;
                case 5:
                    display.SetText($"Unlike electric charges which can be isolated, the two magnetic poles always come in a pair.");
                    break;
                case 6:
                    display.SetText($"When you break the bar magnet, two new bar magnets are obtained, each with a north pole and a south pole.");
                    break;
                case 7:
                    display.SetText($"In other words, magnetic “monopoles” do not exist in isolation, although they are of theoretical interest.");
                    break;
                case 8:
                    display.SetText($"Magnetic fields may be represented by quantities called vectors that have direction and magnitude.");
                    break;
                case 9:
                    display.SetText($"Magnetic fields are created by electric currents in the space around where the currents flow.");
                    break;
                case 10:
                    display.SetText("Currents which do not change with time (called direct currents or DC) make constant magnetic fields which we call DC fields.");
                    break;
                case 11:
                    display.SetText($"Currents which change sign in a regular manner with time are called alternating currents or AC.");
                    break;
                case 12:
                    display.SetText($"Magnetic fields play an important role in our society.");
                    break;
                case 14:
                    display.SetText($"A change in magnetic field generates a current that powers a lot of electronic appliances.");
                    break;
            }
        }
        // Else, the electric field is presented
        else
        {
            pageNumber = controller.electricFieldDescriptionPageNum % numOfElectricFieldDescriptionPages;
            if (pageNumber < 0)
            {
                pageNumber += numOfElectricFieldDescriptionPages;
            }

            switch(pageNumber)
            {
                case 0:
                    display.SetText($"This section provides background information on electric fields.");
                    break;
                case 1:
                    display.SetText($"An electric field is the physical field that surrounds electrically-charged particles");
                    break;
                case 2:
                    display.SetText($"It exerts force on all other charged particles in the field, either attracting or repelling them.");
                    break;
                case 3:
                    display.SetText($"Electric fields originate from electric charges, or from changing magnetic fields.");
                    break;
                case 4:
                    display.SetText($"The electric field is a vector field associated with the Coulomb force experienced by a test charge at a point.");
                    break;
                case 5:
                    display.SetText($"If the field is created by a positive charge, the electric field will be in radially outward direction.");
                    break;
                case 6:
                    display.SetText($"if the field is created by negative charge, the electric field will be in radially inwards direction.");
                    break;
                case 7:
                    display.SetText($"The formula of electric field is given as:");
                    break;
                case 8:
                    display.SetText($"E = F / Q, where F is the force exerted on the charge Q.");
                    break;
                case 9:
                    display.SetText($"Michael Faraday discovered electromagnetic induction in the year of 1831.");
                    break;
                case 10:
                    display.SetText($"He generated a current using a coil and a bar magnet.");
                    break;
                case 11:
                    display.SetText($"The explanation behind electromagnetic induction is summarized by Faraday's Law.");
                    break;
                case 12:
                    display.SetText($"Faraday's Law states that a changing magnetic field creates an electric field.");
                    break;
                case 13:
                    display.SetText($"There will be an electromagnetic induction demo in the next room.");
                    break;
            }
        }
            
           
    }
        
        

}
