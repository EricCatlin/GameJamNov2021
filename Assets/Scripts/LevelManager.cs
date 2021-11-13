using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject GameManagerObject;

    public GameManager gameManager;

    public GameObject FailureUI;

    public int timeout = 10;

    public bool Won;

    // Start is called before the first frame update
    void Start()
    {
        // Find GameManager
        GameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        gameManager = GameManagerObject.GetComponent<GameManager>();

        // Inform GameManager this this script has loaded and is ready to begin
        gameManager.LevelLoaded(this);
    }

    public void Begin()
    {
        Debug.Log("Level Started");
        StartCoroutine(Timeout());
    }

    public void Complete()
    {
        Debug.Log("Level Completed");
        Won = true;
    }

    public void Failed()
    {
        Won = false;
        Debug.Log("Level Failed");
        FailureUI.SetActive(true);
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(2.5f);
        if (Won)
        {
            if (gameManager != null)
            {
                gameManager.LevelFinished(1);
            }
        }
        else
        {
            if (gameManager != null)
            {
                gameManager.LevelFinished(0);
            }
        }
    }

    IEnumerator Timeout()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeout);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        Failed();
    }
}
