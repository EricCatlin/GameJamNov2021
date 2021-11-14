using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    // Get GameManager
    GameManager gameManager;

    // Get TextMeshPro
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Setup()
    {
        // Find gameManager
        gameManager = GameObject.FindObjectOfType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Final Score: " + gameManager.score;
    }

    // Update is called once per frame
}
