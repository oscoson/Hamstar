using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlanet : Planet
{

    public override Vector2 GetPullForce(Rigidbody2D rb)
    {
        const float G = 100;
        Vector2 rawDirection = planetRb.position - rb.position;
        Vector2 normDirection = rawDirection.normalized;
        float lengthSqr = rawDirection.SqrMagnitude();

        return (G * rb.mass * planetRb.mass / lengthSqr) * normDirection;
    }
}
