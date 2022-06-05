using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PathCreation;

public class Rail : MonoBehaviour
{
    [SerializeField] private GameObject[] objs;
    private PathCreator pathCreator;
    [SerializeField] private float speed = 1.0f;
    private float[] speeds;
    private float[] distances;
    // Start is called before the first frame update
    void Start()
    {
        pathCreator = GetComponent<PathCreator>();
        distances = new float[objs.Length];
        speeds = new float[objs.Length];
        Array.Fill(speeds, speed);
        for(int i = 0; i < distances.Length; ++i)
        {
            distances[i] = pathCreator.path.GetClosestDistanceAlongPath(objs[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < distances.Length; ++i)
        {
            distances[i] += speeds[i] * Time.deltaTime;
            if (pathCreator.path.isClosedLoop)
            {
                if (distances[i] >= pathCreator.path.length) distances[i] -= pathCreator.path.length;
                else if (distances[i] < 0.0f) distances[i] += pathCreator.path.length;
                objs[i].transform.position = pathCreator.path.GetPointAtDistance(distances[i]);
            }
            else
            {
                if (distances[i] >= pathCreator.path.length)
                {
                    distances[i] = pathCreator.path.length;
                    speeds[i] *= -1;
                }
                else if (distances[i] < 0.0f)
                {
                    distances[i] = 0.0f;
                    speeds[i] *= -1;
                }
                objs[i].transform.position = pathCreator.path.GetPointAtDistance(distances[i], EndOfPathInstruction.Stop);
            }
        }



        


    }
}
