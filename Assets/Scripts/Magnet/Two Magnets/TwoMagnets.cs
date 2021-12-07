using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TwoMagnets: MonoBehaviour
{
    // Two Magnets mini scene variables

    // The poles are needed to determine the invisible plane that divides the dynamic magnet into two equal cubes
    [SerializeField]
    GameObject northPoleDynamicMagnet;

    [SerializeField]
    GameObject southPoleDynamicMagnet;

    // In the update, we will compute the electrostatic force
    // Thus, we assume that the south pole / right pole of the dynamic magnet is one charge
    // and the north pole / left pole of the static magnet to be the other charge in the equation
    // We reference the north pole of the static magnet in the serialized field below
    [SerializeField]
    GameObject northPoleStaticMagnet;
    
    [SerializeField]
    Material material;
    float pointPos;

    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField]
    ControllerButton controller;

    // We know that throughout the game and across all the scripts that references magnets
    // the magnitude of the charges are the same namely +3 for the negative charge and -3 for the positive charge.
    
    // DISCLAIMER
    // Ideally, we would just reference these charges in the Magnetic.cs file. But, I am running out of time at the time of writing this.
    // Thus, I will create variables that represent these charges.
    // The impact of this is that
    // If the charges need to be changed for some reasons, the visualization of the field will no longer be accurate.

    float positiveCharge = 3;
    float negativeCharge = -3;

    // Coulomb's constant
    float k = 9 * 10^9;

    void Update()
    {
        // Set the shader to clip the vectors that are behind the magnet
        pointPos = (northPoleDynamicMagnet.transform.position.z + southPoleDynamicMagnet.transform.position.z) / 2;
        material.SetFloat("pointPos", pointPos);

        
        // Computing the electrostatic force equation
        float distanceSquared = Mathf.Pow(Mathf.Abs(southPoleDynamicMagnet.transform.position.z - northPoleStaticMagnet.transform.position.z),2);
        float force = Mathf.Abs(k * positiveCharge * negativeCharge / distanceSquared);
        

        int pageNumber = controller.pageNumber % 14;
        if (pageNumber < 0)
        {
            pageNumber += 14;
        }
        switch(pageNumber)
        {
            case 0:
                display.SetText($"This exhibit presents a scenario in which two magnets generate a magnetic field.");
                break;
            case 1:
                display.SetText($"We have seen the magnetic field of a single moving bar magnet in the previous exhibit.");
                break;
            case 2:
                display.SetText($"Now, what happens to the field visualizations when a magnet is moving into or away from another magnet?");
                break;
            case 3:
                display.SetText($"Textbooks often picture that there is a constant number of magnetic field lines between two magnets.");
                break;
            case 4:
                display.SetText($"That kind of visualization is misleading.");
                break;
            case 5:
                display.SetText($"It is misleading because there is no particular number that represents the number of magnetic field lines.");
                break;
            case 6:
                display.SetText($"A key concept this exhibit illustrates is that magnetic field lines always fill in the space between the magnets.");
                break;
            case 7:
                display.SetText($"Another key concept demonstrated in this exhibit is electrostatic force.");
                break;
            case 8:
                display.SetText($"Electrostatic force is the force between two point charges.");
                break;
            case 9:
                display.SetText($"The ideas behind it is summarized by Coulomb's Law, as follows:");
                break;
            case 10:
                display.SetText($"the magnitude of the electrostatic force of attraction or repulsion between two point charges is ...");
                break;
            case 11:
                display.SetText($"directly proportional to the product of the charges,");
                break;
            case 12:
                display.SetText($"and inversely proportional to the square of the distance between them.");
                break;
            case 13:
                display.SetText($"Assuming that the charges are -3 and 3, the magnitude of the electrostatic force is {force} N");
                break;
        }
    }
}
