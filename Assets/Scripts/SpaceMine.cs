using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMine : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private float blinkTimer;
    private const float blinkTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        blinkTimer = blinkTime;
    }

    // Update is called once per frame
    void Update()
    {
        blinkTimer -= Time.deltaTime;
        if (blinkTimer < 0.2f)
        {
            spriteRenderer.color = new Color(1, 0.25f, 0.25f, 1);
            if(blinkTimer < 0.0f)
            {
                blinkTimer = blinkTime;
                spriteRenderer.color = Color.white;

            }
        } else
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }
}
