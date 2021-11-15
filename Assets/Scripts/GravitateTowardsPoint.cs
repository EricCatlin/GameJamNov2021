using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateTowardsPoint : MonoBehaviour
{
    public Transform target;

    public float speed = 1;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Missle Movement Logic
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        float force = speed;
        Vector3 forceVector = direction.normalized * force;

        if (!rb.isKinematic)
        {
            rb.AddForce (forceVector);
        }
        else
        {
            rb.velocity =
                Vector3.Lerp(rb.velocity, forceVector, Time.deltaTime);
        }
    }
}
