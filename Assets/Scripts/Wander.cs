using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Wander smoothly around the scene in 2d space
        transform.position =
            new Vector3(Mathf.Sin(Time.time * 2.0f),
                Mathf.Cos(Time.time),
                0.0f);
    }
}
