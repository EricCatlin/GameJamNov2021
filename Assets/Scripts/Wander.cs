using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public Rigidbody2D rb;

    public float offset = 0.5f;

    public float scale = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        offset = Random.Range(0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        // Wander smoothly around the scene in 2d space
        rb.velocity =
            new Vector2(Mathf.Sin((offset + Time.time) * scale),
                Mathf.Cos((offset + Time.time) * scale));
    }
}
