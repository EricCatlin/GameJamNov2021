using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Smoothly move in 2d with wasd
        transform
            .Translate(new Vector3(Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0) *
            Time.deltaTime *
            10);
    }
}
