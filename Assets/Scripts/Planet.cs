using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planet : MonoBehaviour
{
    protected Rigidbody2D planetRb;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        planetRb = GetComponent<Rigidbody2D>();
        var circleCollider = GetComponent<CircleCollider2D>();
        float radius = circleCollider.radius * transform.localScale.x;
        planetRb.mass = Mathf.PI * radius * radius;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public abstract Vector2 GetPullForce(Rigidbody2D rb);
}
