using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{
    public GameObject explosion;

    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);

        // Start Coroutine
        StartCoroutine(Explosion());

        // Remove all components from gameObject with destroying it
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject.GetComponent<GravitateTowardsPoint>());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2.5f);

        Destroy (gameObject);
        levelManager.Complete();
    }
}
