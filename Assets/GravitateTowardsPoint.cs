using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateTowardsPoint : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Missle Movement Logic
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        float force = .3f;
        Vector3 forceVector = direction.normalized * force;
        GetComponent<Rigidbody2D>().AddForce((Vector2) forceVector);
    }
}
