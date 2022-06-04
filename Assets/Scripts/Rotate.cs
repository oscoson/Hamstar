using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    // Update is called once per frame
    private void FixedUpdate() 
    {
        transform.Rotate(0, 2.5f, 0);
    }
}
