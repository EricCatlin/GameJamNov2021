using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public Rigidbody2D rb;

    public float offset = 0.5f;

    public float scale = 0.5f;

    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //  offset = Random.Range(0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        // Missle Movement Logic
        Vector3 direction =
            Mathf.Cos(Time.time * scale + offset) * Vector3.right +
            Mathf.Sin(Time.time * scale + offset) * Vector3.up;
        Vector3 forceVector = direction.normalized * speed;

        // Lerp To new velocit
        rb.velocity = Vector3.Lerp(rb.velocity, forceVector, Time.deltaTime);
    }
}
