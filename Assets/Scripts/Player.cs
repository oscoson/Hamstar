using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip[] boostSfxs; 
    [SerializeField] AudioClip launchSfx;
    [SerializeField] AudioClip crashLandSfx;


    [SerializeField] private float launchForce = 9.0f;
    [SerializeField] private float boostForce = 3.0f;

    [SerializeField] private bool launched = false;
    [SerializeField] private bool offPlanet = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && DistanceToClosestPlanet() < PlayerPlanetRadiusSum() + 0.2f)
        {
            Launch();
        } 
        if (Input.GetButtonDown("Fire2") && DistanceToClosestPlanet() < PlayerPlanetRadiusSum() + 0.2f) 
        {
            Boost();
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Planet")
        {
            launched = false;
            offPlanet = false;
        }
        Vector2 towardsPlayer = transform.position - other.transform.position;
        if (other.relativeVelocity.magnitude > 6.0f && playerRB.velocity.magnitude < 7.0f)
        {
            animator.SetTrigger("Collide");
            audioSource.PlayOneShot(crashLandSfx);
        }
        


        if (other.gameObject.tag == "Goal")
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (++buildIndex == SceneManager.sceneCountInBuildSettings) buildIndex = 0;
            SceneManager.LoadScene(buildIndex);
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Planet")
        {
            offPlanet = true;
            launched = true;
        }       
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Launch()
    {
        
        launched = true;
        Planet closestPlanet = GetClosestPlanet();
        
        playerRB.AddForce(launchForce * (playerRB.position - (Vector2) closestPlanet.transform.position).normalized, ForceMode2D.Impulse);
        //rb.AddForce(((launchForce)) * (playerRB.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized, ForceMode2D.Impulse);

        audioSource.PlayOneShot(launchSfx);
    }

    public void Boost()
    {

        Planet closestPlanet = GetClosestPlanet();

        Vector2 referenceVector = Quaternion.Euler(0, 0, 90) * (transform.position - closestPlanet.transform.position).normalized;

        if(Vector2.Dot(referenceVector, playerRB.velocity) > 0)
        {
            playerRB.AddForce(boostForce * referenceVector, ForceMode2D.Impulse);
        }
        else
        {
            playerRB.AddForce(-boostForce * referenceVector, ForceMode2D.Impulse);
        }
        audioSource.PlayOneShot(boostSfxs[Random.Range(0, boostSfxs.Length)]);
    }

    private Planet GetClosestPlanet()
    {
        Planet[] planets = GameObject.FindObjectsOfType<Planet>();
        return planets.OrderBy(planet => ((Vector2)(planet.transform.position - transform.position)).sqrMagnitude).First();
    }

    private float DistanceToClosestPlanet()
    {
        return ((Vector2)(GetClosestPlanet().transform.position - transform.position)).magnitude;
    }

    private float PlayerPlanetRadiusSum()
    {
        return playerRB.GetComponent<CircleCollider2D>().radius * transform.localScale.x + GetClosestPlanet().GetComponent<CircleCollider2D>().radius * GetClosestPlanet().transform.localScale.x;
    }
}
