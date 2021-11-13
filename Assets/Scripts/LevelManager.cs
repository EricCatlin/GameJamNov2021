using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public GameObject GameManagerObject;

    public GameObject CountDownUI;

    public GameManager gameManager;

    public GameObject FailureUI;

    public UnityEvent OnBegin;

    public UnityEvent OnLevelComplete;

    public UnityEvent OnLevelFailed;

    public int timeout = 10;

    public Coroutine timeoutCoroutine;

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
        timeoutCoroutine = StartCoroutine(Timeout());
    }

    public void End()
    {
        Debug.Log("Level Ended");
        StopCoroutine (timeoutCoroutine);
    }

    public void Complete()
    {
        Debug.Log("Level Completed");
        Won = true;
        OnLevelComplete.Invoke();
        End();
    }

    public void Failed()
    {
        Debug.Log("Level Failed");
        Won = false;
        FailureUI.SetActive(true);
        OnLevelFailed.Invoke();
        End();
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
        // Create a countdown UI element
        CountDownUI.SetActive(true);

        //yield on a new YieldInstruction that waits for N seconds.
        while (timeout > 0)
        {
            foreach (Transform child in CountDownUI.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            for (int i = Mathf.Min(10, timeout); i > 0; i--)
            {
                // Create a Primitive square sprite
                GameObject square =
                    GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(square.GetComponent<Collider>());
                square.transform.parent = CountDownUI.transform;
                square.transform.position =
                    CountDownUI.transform.position + Vector3.right * .2f * i;
                square.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
            yield return new WaitForSeconds(1);
            timeout--;
        }

        if (Won)
        {
            Complete();
        }
        else
        {
            Failed();
        }
    }
}
