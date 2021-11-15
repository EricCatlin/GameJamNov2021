using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FinalScore : MonoBehaviour
{
    // Get GameManager
    public LevelManager levelManager;

    public GameManager gameManager;

    public float delay = 0.01f;

    // Get TextMeshPro
    TextMeshProUGUI text;

    public UnityEvent onComplete;

    public UnityEvent onPerfect;

    private string[]
        status =
        { "Perfect", "Great", "Ok", "Not Well", "Pretty Bad", "Terrible" };

    // Start is called before the first frame update
    public void Setup()
    {
        // Find gameManager
        gameManager = levelManager.gameManager;
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(SayThings());
    }

    IEnumerator SayThings()
    {
        float ratio = (float) gameManager.score / (float) gameManager.maxScore;
        Debug.Log("Ratio: " + ratio);
        Debug.Log("score: " + gameManager.score);
        Debug.Log("MaxScore: " + gameManager.maxScore);

        while (delay < 1f)
        {
            // Wait for 1 second
            yield return new WaitForSeconds(delay);
            text.text = status[Random.Range(0, status.Length)];
            delay *= 1.3f;
            // Set text
        }

        onComplete.Invoke();
        if (ratio > 0.99f)
        {
            text.text = "Perfect";
            onPerfect.Invoke();
        }
        else if (ratio > 0.8f)
        {
            text.text = "Great";
        }
        else if (ratio > 0.5f)
        {
            text.text = "Ok";
        }
        else if (ratio > 0.2f)
        {
            text.text = "Not Well";
        }
        else if (ratio > 0.1f)
        {
            text.text = "Pretty Bad";
        }
        else
        {
            text.text = "Terrible";
        }
    }
}
