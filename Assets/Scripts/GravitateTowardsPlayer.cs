using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateTowardsPlayer : MonoBehaviour
{
    // Intance of the player
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Get player by tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Move slowly towards player
        transform.position =
            Vector3
                .MoveTowards(transform.position,
                player.transform.position,
                0.01f);
    }
}
