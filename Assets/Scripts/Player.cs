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
    SpriteRenderer spriteRenderer;


    [SerializeField] private float launchForce = 9.0f;
    [SerializeField] private float boostForce = 3.0f;
    private bool isLevelTransitioning = false;
    private Transform spawnPoint;

    public GameObject hamsterDeathEffect;
    public Animator transitionControl;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        GameObject spawnPointObject = new GameObject();
        spawnPointObject.transform.position = transform.position;
        spawnPoint = spawnPointObject.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
        }

        if (!isLevelTransitioning)
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
    }

    private void FixedUpdate()
    {
        if (!isLevelTransitioning)
        {
            Planet[] planets = GameObject.FindObjectsOfType<Planet>();
            Debug.Log(planets.Length);

            Vector2 netForce = Vector2.zero;
            foreach (var planet in planets)
            {
                Debug.Log(planet);
                netForce += planet.GetPullForce(playerRB);
                Debug.Log(netForce);

            }
            playerRB.AddForce(netForce);
        } 

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {

        Vector2 towardsPlayer = transform.position - other.transform.position;
        if (other.relativeVelocity.magnitude > 6.0f && (playerRB.velocity.magnitude < 7.0f
            || other.gameObject.GetComponent<BumpyPlanet>()))
        {
            animator.SetTrigger("Collide");
            audioSource.PlayOneShot(crashLandSfx);
        }
        
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Planet")
        {
                //Empty
        }       
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
        
    }

    IEnumerator DieCoroutine()
    {
        Instantiate(hamsterDeathEffect, transform.position, Quaternion.identity);
        playerRB.simulated = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(1.0f);
        spriteRenderer.enabled = true;

        playerRB.simulated = true;
        transform.position = spawnPoint.position;
        playerRB.velocity = Vector3.zero;
        this.GetComponent<TrailRenderer>().Clear();
    }

    public void Launch()
    {
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

    public void SetVelocity(Vector2 velocity)
    {
        playerRB.velocity = velocity;
    }

    public void StartLevelTransition(Transform endTransform)
    {
        isLevelTransitioning = true;
        playerRB.simulated = false;
        transitionControl.SetTrigger("Start");
        StartCoroutine(LevelTransitionCoroutine(endTransform, transform.position, playerRB.rotation));
    }

    IEnumerator LevelTransitionCoroutine(Transform endTransform, Vector2 startPosition, float startingRotation)
    {
        const float snapSpeed = 2.0f;
        Vector2 travelDirection = endTransform.position - transform.position;
        Vector2 futureDirection = (Vector2)endTransform.position - ((Vector2)transform.position + travelDirection.normalized * snapSpeed * Time.deltaTime);
        while (Vector2.Dot(travelDirection,futureDirection) > 0.0f)
        {
            travelDirection = endTransform.position - transform.position;
            futureDirection = (Vector2)endTransform.position - ((Vector2)transform.position + travelDirection.normalized * snapSpeed * Time.deltaTime);

            Vector2 refVec = (Vector2) endTransform.position - startPosition;
            Vector2 casterVec = (Vector2)transform.position - startPosition;
            Vector2 projVec = Vector2.Dot(casterVec, refVec.normalized) * refVec.normalized;
            float lerpRatio = projVec.magnitude / refVec.magnitude;

            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(startingRotation, 0, lerpRatio));

            transform.position = (Vector2) transform.position + travelDirection.normalized * snapSpeed * Time.deltaTime;
            yield return null;
        }

        while(transform.localScale.x > 0.0f)
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (++buildIndex == SceneManager.sceneCountInBuildSettings) buildIndex = 0;
        SceneManager.LoadScene(buildIndex);

    }

    private Planet GetClosestPlanet()
    {
        Planet[] planets = GameObject.FindObjectsOfType<Planet>();

        return planets.OrderBy(planet => 
        ((Vector2)(planet.transform.position - transform.position)).magnitude -
        (planet.transform.GetComponent<CircleCollider2D>().radius * planet.transform.localScale.x +
        transform.GetComponent<CircleCollider2D>().radius * transform.localScale.x)
        ).First();
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
