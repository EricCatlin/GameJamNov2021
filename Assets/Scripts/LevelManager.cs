using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public GameObject GameManagerObject;

    public GameObject CountDownUI;

    public GameManager gameManager;

    public UnityEvent OnLevelComplete;

    public UnityEvent OnLevelFailed;

    public UnityEvent OnSetup;

    public UnityEvent OnBegin;

    public UnityEvent OnTearDown;

    public int playTimeout = 10;

    Coroutine gameTimer;

    Coroutine setupTimer;

    Coroutine tearDownTimer;

    public bool Won;

    public int score = 0;

    public void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        Debug.Log("LevelManager Start " + gameManager.gameObject.name);
        gameManager.LevelLoaded(this);
    }

    // <SETUP>
    public void Setup()
    {
        OnSetup.Invoke();
        setupTimer = StartCoroutine(SetupTimeout());
    }

    public IEnumerator SetupTimeout()
    {
        yield return new WaitForSeconds(2);
        SetupComplete();
    }

    public void SetupComplete()
    {
        if (setupTimer != null)
        {
            StopCoroutine (setupTimer);
            setupTimer = null;
        }
        if (gameManager != null)
        {
            gameManager.LevelReady(this);
        }
    }

    // </SETUP>
    // <Game>
    public void Play()
    {
        OnBegin.Invoke();
        if (playTimeout > 0)
        {
            gameTimer = StartCoroutine(PlayTimeout());
        }
    }

    public IEnumerator PlayTimeout()
    {
        // Create a countdown UI element
        CountDownUI.SetActive(true);

        //yield on a new YieldInstruction that waits for N seconds.
        while (playTimeout > 0)
        {
            foreach (Transform child in CountDownUI.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            for (int i = Mathf.Min(10, playTimeout); i > 0; i--)
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
            playTimeout--;
        }
        PlayComplete();
    }

    public void SetWon(bool won)
    {
        Won = won;
    }

    public void PlayComplete()
    {
        if (gameTimer != null)
        {
            StopCoroutine (gameTimer);
            gameTimer = null;
        }

        if (Won)
        {
            Debug.Log("Level Completed");
            Won = true;
            OnLevelComplete.Invoke();
            gameManager.LevelComplete(this, true);
        }
        else
        {
            Debug.Log("Level Failed");
            Won = false;
            OnLevelFailed.Invoke();
            gameManager.LevelComplete(this, false);
        }
    }

    // </Game>
    // <TearDown>
    public void TearDown()
    {
        OnTearDown.Invoke();
        tearDownTimer = StartCoroutine(TearDownTimeout());
    }

    public IEnumerator TearDownTimeout()
    {
        yield return new WaitForSeconds(3);
        TearDownComplete();
    }

    public void TearDownComplete()
    {
        if (tearDownTimer != null)
        {
            StopCoroutine (tearDownTimer);
            tearDownTimer = null;
        }
        if (gameManager != null)
        {
            gameManager.score += score;
            gameManager.LevelUnloaded(this);
        }
    }

    // </TearDown>
    public void SetScore(int score)
    {
        this.score = score;
    }
}
