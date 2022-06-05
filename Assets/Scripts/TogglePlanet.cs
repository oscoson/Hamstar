using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TogglePlanet : Planet
{
    [SerializeField] private bool isRepelling = false;
    private float pullFactor = 1.0f;
    SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.cyan;
        pullFactor = isRepelling ? -1.0f : 1.0f;
        if (pullFactor > 0.0f)
        {
            spriteRenderer.color = Color.cyan;
        } else
        {
            spriteRenderer.color = Color.yellow;
        }
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pullFactor *= -1;
            if(pullFactor > 0.0f)
            {
                spriteRenderer.color = Color.cyan;
            } else
            {
                spriteRenderer.color = Color.yellow;
            }
        }
    }

    public override Vector2 GetPullForce(Rigidbody2D rb)
    {
        const float G = 20;
        Vector2 rawDirection = planetRb.position - rb.position;
        Vector2 normDirection = rawDirection.normalized;
        float lengthSqr = rawDirection.SqrMagnitude();

        return pullFactor * (G * rb.mass * planetRb.mass / lengthSqr) * normDirection;
    }

}
