using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject GameManagerObject;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Find GameManager
        GameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        gameManager = GameManagerObject.GetComponent<GameManager>();
        gameManager.SetLevelFromScene(this);
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

        if (gameManager != null)
        {
            Debug.Log("LevelManager: GameManager found");
            gameManager.Complete(2);
        }
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
