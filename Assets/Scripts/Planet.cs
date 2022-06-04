using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planet : MonoBehaviour
{
    // Start is called before the first frame update
    protected void virtual Start()
    {
        
    }

    // Update is called once per frame
    protected void virtual Update()
    {
        
    }

    public abstract Vector2 GetPullForce();
}
