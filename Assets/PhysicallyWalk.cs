using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class PhysicallyWalk : MonoBehaviour
{
    public int framesPerStep = 10;

    private int currentFrame = 0;

    public Vector2 stepSize = Vector2.one;

    private Vector2 leadingFoot = Vector2.zero;

    private Vector2 trailingFoot = Vector2.zero;

    public Vector2 leadingStrength = Vector2.one;

    public Vector2 trailingStrength = Vector2.one;

    public Vector2 PlanarOffset = Vector2.zero;

    //Require a rigidbdoy2D component
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Get Hoizontal and Vertical inputs
        Vector2 inputs =
            new Vector2(Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));

        if (Vector2.Distance(trailingFoot, rb.position) > stepSize.magnitude)
        {
            ShiftWeight (inputs);
        }

        // Get normlaizedDirection towards frontFoot
        Vector2 direction = inputs;

        // Get distance to frontFoot
        float leadingDistance = Vector2.Distance(leadingFoot, rb.position);
        float trailingDistance = Vector2.Distance(trailingFoot, rb.position);

        float ls = Mathf.Max(0, 1 - (stepSize.magnitude - leadingDistance));
        float ts = Mathf.Max(0, 1 - (stepSize.magnitude - trailingDistance));

        // Get force to apply
        Vector2 force = direction * (ls + ts) * rb.mass;
        Debug.DrawRay(transform.position, force, Color.green);

        rb.AddForce (force);
    }

    void ShiftWeight(Vector2 inputs)
    {
        Debug.Log("Shift");

        // Swap leading and trailing foot
        Vector2 temp = leadingFoot;
        leadingFoot = trailingFoot;
        trailingFoot = temp;

        // Get the closest grid point in the direction of input
        leadingFoot = rb.position + (inputs * stepSize);

        // calculate the step direction and size based on the input and the step size and offset
    }

    // Draw Gizmo at world point
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw point at
        Gizmos.DrawWireSphere(leadingFoot, .2f);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(trailingFoot, .2f);
    }
}
