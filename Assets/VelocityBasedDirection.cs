using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityBasedDirection : MonoBehaviour
{
    Rigidbody2D rb;

    SpriteRenderer sr;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get rigidbody2d
        if (sr && rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (sr && rb.velocity.x < 0)
        {
            sr.flipX = true;
        }

        if (anim != null && rb.velocity.magnitude > .1f)
        {
            anim.enabled = (true);
        }
        else if (anim != null)
        {
            anim.enabled = (false);
        }
    }
}
