using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Rail : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private PathCreator pathCreator;
    [SerializeField] private float speed = 1.0f;
    private float distance = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        pathCreator = GetComponent<PathCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance += speed * Time.deltaTime;

        

        if (pathCreator.path.isClosedLoop)
        {
            if (distance >= pathCreator.path.length) distance -= pathCreator.path.length;
            else if (distance < 0.0f) distance += pathCreator.path.length;
            obj.transform.position = pathCreator.path.GetPointAtDistance(distance);
        }
        else
        {
            if (distance >= pathCreator.path.length)
            {
                distance = pathCreator.path.length;
                speed *= -1;
            }
            else if (distance < 0.0f)
            {
                distance = 0.0f;
                speed *= -1;
            }
            obj.transform.position = pathCreator.path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
        }


    }
}
