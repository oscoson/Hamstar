using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHam : MonoBehaviour
{
    private float randNum;

    void Start()
    {
        randNum = Random.value;
    }
    // Update is called once per frame
    void Update()
    {
        if(randNum > 0.5f)
        {
            transform.Rotate(Vector3.forward * -0.5f);
        }
        else
        {
            transform.Rotate(Vector3.forward * 0.5f);
        }

    }

}
