using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPlanet : Planet
{

    public override Vector2 GetPullForce(Rigidbody2D rb)
    {
        const float G = 20;
        Vector2 rawDirection = planetRb.position - rb.position;
        Vector2 normDirection = rawDirection.normalized;
        float lengthSqr = rawDirection.SqrMagnitude();

        return (G * rb.mass * planetRb.mass / lengthSqr) * normDirection;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.StartLevelTransition(transform);
            //int buildIndex = SceneManager.GetActiveScene().buildIndex;
            //if (++buildIndex == SceneManager.sceneCountInBuildSettings) buildIndex = 0;
            //SceneManager.LoadScene(buildIndex);
        }
    }
}
