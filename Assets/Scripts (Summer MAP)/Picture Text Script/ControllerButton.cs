using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControllerButton : MonoBehaviour
{
    public int pageNumber = 0;
    public void NextPage()
    {
        pageNumber += 1;
    }

    // Update is called once per frame
    public void PreviousPage()
    {
        pageNumber -= 1;
    }
}
