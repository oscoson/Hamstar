using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalStar : MonoBehaviour
{
    Rigidbody2D starRb;
    private float flicktimer;
    private float flicktime;
    // Start is called before the first frame update
    void Start()
    {
        starRb = GetComponent<Rigidbody2D>();
        flicktime = Random.Range(1.0f, 10.0f);
        flicktimer = flicktime;
    }

    // Update is called once per frame
    void Update()
    {
        flicktimer -= Time.deltaTime;
        if(flicktimer <= 0.0f)
        {
            Flick();
            flicktime = Random.Range(6.0f, 15.0f);
            flicktimer = flicktime;
        }
    }

    void Flick()
    {
        float torque = Random.Range(10.0f, 20.0f) * (Random.Range(-1, 1) > 0.0f ? 1.0f : -1.0f);
        Debug.Log(torque);
        starRb.AddTorque(torque, ForceMode2D.Impulse);
    }
}
