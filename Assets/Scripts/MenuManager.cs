using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject GameManagerObject;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Find GameManager
        GameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        gameManager = GameManagerObject.GetComponent<GameManager>();
    }

    public void Complete()
    {
        Debug.Log("Start Game");
        if (gameManager != null)
        {
            gameManager.StartGame();
        }
    }
}
