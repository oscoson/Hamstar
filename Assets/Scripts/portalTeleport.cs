using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleport : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    public float distance = 0.2f;
    private float timeStart = 1; // 1 Second teleportation cooldown
    private float timer = 0;

    void Teleport(Collider2D Player){
        Debug.Log("Teleported Hamster lmao");
        Player.transform.position = new Vector2(destination.transform.position.x, destination.transform.position.y);
        timer = timeStart;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // Delta time evens out the time
    void Update()
    {
        timer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            if(other.gameObject.CompareTag("Player")){
                if(timer <= 0){
                    Teleport(other);
                }
            }
        }
    }
}
