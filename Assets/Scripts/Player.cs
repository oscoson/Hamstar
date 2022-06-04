using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerRB;
    [SerializeField] private float launchForce = 2000.0f;

    [SerializeField] private bool launched = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        Planet[] planets = GameObject.FindObjectsOfType<Planet>();

        Vector2 netForce = Vector2.zero;
        foreach (var planet in planets){
            netForce += planet.GetPullForce(playerRB);
        }

        playerRB.AddForce(netForce);

    }

    public void Launch()
    {
        
        launched = true;
        Rigidbody2D rb = playerRB.GetComponent<Rigidbody2D>();
        rb.AddForce(((launchForce)) * (playerRB.position - (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized, ForceMode2D.Impulse);
    }
}
