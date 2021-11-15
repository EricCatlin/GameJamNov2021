using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDController : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 10f;

    public bool H = true;

    public bool V = true;

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

        if (!H) horizontal = 0;
        if (!V) vertical = 0;

        if (rb.isKinematic)
        {
            rb.velocity =
                Vector2
                    .Lerp(rb.velocity,
                    new Vector2(horizontal * speed, vertical * speed),
                    0.9f);
        }
        else
        {
            // Move rb with forces in the direction of the input
            // Force is a vector
            // Force is a vector
            Vector2 forceVector = new Vector2(horizontal, vertical);

            // Normalize the vector to get a unit vector
            forceVector.Normalize();

            // Multiply the unit vector by the force
            forceVector *= speed;

            // Add the force to the rigidbody
            rb.AddForce (forceVector);

            // Prevent the character from moving faster than the speed
            // Limit the speed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);

            // Slow down if the character horizontal or vertical is 0
            // If the character is not moving, slow down
            if (horizontal == 0 && vertical == 0)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
            }
        }
    }
}
