using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRB;
    [SerializeField] private float launchSpeed = 5f;

    [SerializeField] private bool launched = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !(launched))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        var thing = GameObject.FindObjectOfType<Planet>();


    }

    public void Launch()
    {
        launched = true;
        Rigidbody2D rb = playerRB.GetComponent<Rigidbody2D>();
        rb.AddForce(-(launchSpeed) * playerRB.position, ForceMode2D.Impulse);
    }
}
