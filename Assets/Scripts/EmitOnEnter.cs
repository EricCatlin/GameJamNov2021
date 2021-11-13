using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EmitOnEnter : MonoBehaviour
{
    // Instantiate Unity Event
    public UnityEvent OnEnter;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");

        // If the object is tagged as "Player"
        if (other.gameObject.tag == "Player")
        {
            // Invoke the Unity Event
            OnEnter.Invoke();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }
}
