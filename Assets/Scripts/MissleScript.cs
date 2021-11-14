using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{
    public GameObject explosion;

    public void Explode()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);

        // Remove all components from gameObject with destroying it
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject.GetComponent<GravitateTowardsPoint>());

        yield return new WaitForSeconds(2.5f);
        Destroy (gameObject);
    }
}
