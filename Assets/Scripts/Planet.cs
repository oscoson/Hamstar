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
        var boxCollider = GetComponent<BoxCollider2D>();
        if (circleCollider)
        {
            float radius = circleCollider.radius * transform.localScale.x;
            planetRb.mass = Mathf.PI * radius * radius;
            return;
        }

        planetRb.mass = boxCollider.size.x * boxCollider.size.y;
       
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public abstract Vector2 GetPullForce(Rigidbody2D rb);
}
