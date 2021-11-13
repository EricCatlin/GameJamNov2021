using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtClick : MonoBehaviour
{
    // Get camera
    Camera cam;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //If click is detected
        // Aim towards mouse position
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click detected");
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            Vector3 dir = worldPos - transform.position;
            dir.Normalize();
            transform.right = dir;

            // Instantiate bullet
            bullet =
                Instantiate(bullet, transform.position, Quaternion.identity) as
                GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 10;
        }
    }
}
