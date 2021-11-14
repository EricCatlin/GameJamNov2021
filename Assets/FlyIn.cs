using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIn : MonoBehaviour
{
    public Vector3 FlyInFrom;

    private bool Flying = false;

    public float FlyInSpeed = 10;

    public Vector3 FlyTo;

    public void Start()
    {
        FlyTo = transform.position;
        transform.position += FlyInFrom;
    }

    void Update()
    {
        if (Flying)
        {
            // Move towards the target
            transform.position =
                Vector3
                    .MoveTowards(transform.position,
                    FlyTo,
                    Time.deltaTime * FlyInSpeed);
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
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + FlyInFrom);
    }
}
