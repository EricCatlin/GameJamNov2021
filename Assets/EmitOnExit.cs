using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EmitOnExit : MonoBehaviour
{
    public UnityEvent OnDamageExit;

    public UnityEvent OnPlayerExit;

    public UnityEvent OnAnyExit;

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(gameObject.name + " Exited with " + other.gameObject.name);

        HandleOnExit(other.gameObject.tag, other.gameObject.name);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug
            .Log(gameObject.name +
            " Ended Collision by " +
            other.gameObject.name);
        HandleOnExit(other.gameObject.tag, other.gameObject.name);
    }

    void HandleOnExit(string tag, string name)
    {
        if (tag == "Damage")
        {
            OnDamageExit.Invoke();
        }

        if (tag == "Player")
        {
            OnPlayerExit.Invoke();
        }

        OnAnyExit.Invoke();
    }
}
