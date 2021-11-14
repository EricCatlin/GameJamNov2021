using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOut : MonoBehaviour
{
    public bool Flying = false;

    public float FlyOutSpeed = 10;

    public Vector3 FlyTo;

    public void Start()
    {
    }

    void Update()
    {
        if (Flying)
        {
            // Move towards the target
            transform.position =
                Vector3
                    .MoveTowards(transform.position,
                    transform.position + FlyTo,
                    Time.deltaTime * FlyOutSpeed);
        }
    }

    public void Go()
    {
        Flying = true;
        StartCoroutine(GoCoroutine());
    }

    //Coroutine
    public IEnumerator GoCoroutine()
    {
        yield return new WaitForSeconds(2);
        Flying = false;
    }

    // Draw a gizmo to make it easier to see the path of the fly-in
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + FlyTo);
    }
}
