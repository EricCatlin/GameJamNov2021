using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Find GameManager
        gameManager =
            GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Begin()
    {
        Debug.Log("Level Started");

        // Start a timeout to end the level
        StartCoroutine(Timeout());
    }

    public void Complete()
    {
        Debug.Log("Level Completed");
        gameManager.LoadRandom();
    }

    public void Failed()
    {
        Debug.Log("Level Failed");
    }

    IEnumerator Timeout()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
