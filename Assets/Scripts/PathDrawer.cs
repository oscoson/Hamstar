using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;

public class PathDrawer : MonoBehaviour
{
    PathCreator pathCreator;
    LineRenderer lrenderer;
    public float lineWidth = 0.02f;
    public Color lineColor = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        pathCreator = GetComponent<PathCreator>();
        lrenderer = GetComponent<LineRenderer>();
        lrenderer.positionCount = pathCreator.path.NumPoints;
        lrenderer.startColor = lineColor;
        lrenderer.endColor = lineColor;
        lrenderer.startWidth = lineWidth;
        lrenderer.endWidth = lineWidth;

        List<Vector3> points = new List<Vector3>(pathCreator.path.localPoints);
        var worldPoints = points.Select(x => x + pathCreator.transform.position);
        lrenderer.SetPositions(worldPoints.ToArray());
    }

    // Update is called once per frame
    void Update()
    {

        /* Uncomment these for fun ;) */
        //lrenderer.startColor = new Color(Mathf.Sin(Time.realtimeSinceStartup), Mathf.Sin(Time.realtimeSinceStartup + 10), Mathf.Sin(Time.realtimeSinceStartup + 20));
        //lrenderer.endColor = new Color(Mathf.Sin(Time.realtimeSinceStartup + 30), Mathf.Sin(Time.realtimeSinceStartup + 40), Mathf.Sin(Time.realtimeSinceStartup + 50));
        //lrenderer.startWidth = (int) (Time.realtimeSinceStartup * 2) % 2 == 0 ? 0.03f : 0.1f;
        //lrenderer.endWidth = (int) ( Time.realtimeSinceStartup * 2) % 2 == 0 ? 0.03f : 0.1f;
    }
}
