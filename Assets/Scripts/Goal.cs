using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject goalStar;

    private GameObject goalStars;
    private const int goalStarCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        goalStars = transform.Find("Goal Stars").gameObject;
        for (int i = 0; i < goalStarCount; ++i)
        {
            float angle = (360 / goalStarCount) * i;
            Vector2 unitvec = Quaternion.Euler(0, 0, angle) * Vector2.up;
            Vector2 pos = unitvec + (Vector2)goalStars.transform.position;
            Instantiate(goalStar, pos, Quaternion.identity, goalStars.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        const float rotAngle = 15.0f;
        goalStars.transform.Rotate(Vector3.forward, rotAngle * Time.deltaTime);
        for (int i = 0; i < goalStars.transform.childCount; ++i)
        {
            goalStars.transform.GetChild(i).Rotate(Vector3.forward, -rotAngle * Time.deltaTime);
        }
    }
}
