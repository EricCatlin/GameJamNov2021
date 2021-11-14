using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EmitOnEnter : MonoBehaviour
{
    public UnityEvent OnDamageCollision;

    public UnityEvent OnPlayerCollision;

    public UnityEvent OnAnyCollision;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name + " Triggered by " + other.name);
        HandleOnEnter(other.gameObject.tag, other.gameObject.name);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(gameObject.name + " Collision with " + other.gameObject.name);
        HandleOnEnter(other.gameObject.tag, other.gameObject.name);
    }

    void HandleOnEnter(string tag, string name)
    {
        if (tag == "Damage")
        {
            OnDamageCollision.Invoke();
        }
        if (tag == "Player")
        {
            OnPlayerCollision.Invoke();
        }

        OnAnyCollision.Invoke();
    }
}
