using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get rigidbody2d
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Smoothly move in 2d with wasd
        // Move character using Horzontal and Vertical and 2d physics forces
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.velocity =
            Vector2
                .Lerp(rb.velocity,
                new Vector2(horizontal * 10, vertical * 10),
                0.9f);
        // transform
        //     .Translate(new Vector3(Input.GetAxis("Horizontal"),
        //         Input.GetAxis("Vertical"),
        //         0) *
        //     Time.deltaTime *
        //     10);
    }
}
