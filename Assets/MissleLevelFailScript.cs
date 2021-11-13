using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissleLevelFailScript : MonoBehaviour
{
    public List<GameObject> Houses;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Failed()
    {
        Debug.Log("Failed");
        foreach (GameObject house in Houses)
        {
            Debug.Log("House: " + house.name);
            Rigidbody2D rigid = house.GetComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Dynamic;

            // Add force to the house
            // Get a random between 1 and 10
            float force = Random.Range(1, 10);

            // Get a random between 1 and 10
            float angle = Random.Range(1, 2);

            // Get a random between 1 and 10
            float torque = Random.Range(-.5f, .5f);

            rigid.AddForce(new Vector2(angle, force), ForceMode2D.Impulse);
            rigid.AddTorque(torque, ForceMode2D.Impulse);
        }

        // Start coroutine to wait for a second and then load the next level
        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
        levelManager.Failed();
        Destroy(gameObject.GetComponent<Collider2D>());
    }
}
