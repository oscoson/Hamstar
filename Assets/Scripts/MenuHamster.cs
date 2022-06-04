using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHamster : MonoBehaviour
{
    public GameObject hamsterPrefab;
    private bool spawnStatus = true;

    void Update() 
    {
        if(spawnStatus)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn() 
    {
        spawnStatus = false;
        yield return new WaitForSeconds(1f);
        Vector2 randomSpawn = new Vector2(Random.Range(-8, 8), 7);
        GameObject instanObj = (GameObject) Instantiate(hamsterPrefab, randomSpawn, Quaternion.identity);
        spawnStatus = true;
        Destroy(instanObj, 4f);
    }
}
