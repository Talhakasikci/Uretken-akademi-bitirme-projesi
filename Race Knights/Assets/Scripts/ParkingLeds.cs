using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingLeds : MonoBehaviour
{
    public ParkingControl distance;
    public Material red, green;
    private MeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }
    private void FixedUpdate()
    {
        bool isGreenCheck = distance.isGreen;
        if (isGreenCheck)
        {
            rend.material = green;
        }
        else
        {
            rend.material = red;
        }

    }
}
