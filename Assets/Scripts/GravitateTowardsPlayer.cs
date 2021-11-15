using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateTowardsPlayer : MonoBehaviour
{
    // Intance of the player
    public GameObject player;

    public Rigidbody2D rb;

    public float speed = 10f;

    public float accelleration = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        //Get player by tag
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move rigidbody towards player smoothly
        // Get direction to player
        Vector2 direction = player.transform.position - transform.position;
        direction = direction.normalized * 10;
        rb.velocity = Vector2.Lerp(rb.velocity, direction, 0.01f);
    }
}
