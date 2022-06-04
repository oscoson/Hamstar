using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    Rigidbody2D playerRB;
    [SerializeField] private float launchForce = 10.0f;
    [SerializeField] private float boostForce = 10.0f;
 
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
        Planet[] planets = GameObject.FindObjectsOfType<Planet>();
        Planet closestPlanet = planets.OrderBy(planet => ((Vector2)(planet.transform.position - transform.position)).sqrMagnitude).First();
        
        playerRB.AddForce(launchForce * (playerRB.position - (Vector2) closestPlanet.transform.position).normalized, ForceMode2D.Impulse);
        //rb.AddForce(((launchForce)) * (playerRB.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized, ForceMode2D.Impulse);
    }

}
