using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // Create an array of scenes to be loaded
    public List<SceneField> scenes;

    public List<SceneField> completedScenes;

    public SceneField currentScene;

    public SceneField nextScene;

    public int score = 0;

    // Store a reference to our levelScript which will set up the level.
    private LevelManager CurrentLevel;

    public SceneField MenuScene;

    // Awake is always called before any Start functions
    void Awake()
    {
        Debug.Log("Awake");

        // Check if instance already exists
        if (instance == null)
            // if not, set instance to this
            instance = this; //If instance already exists and it's not this:
        else if (instance != this)
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad (gameObject);

        // Get a component reference to the attached BoardManager script
        if (currentScene == null)
        {
            LoadScene (MenuScene);
        }
    }

    // Initializes the game for each level.
    void InitGame()
    {
        // Call the SetupScene function of the levelScript script, pass it current level number.
        CurrentLevel.Begin();
    }

    void Complete(int score)
    {
        if (CurrentLevel != null)
        {
            this.score += score;
            completedScenes.Add (currentScene);
            scenes.Remove (currentScene);

            // If there are no more levels to load, end the game.
            if (scenes.Count == 0)
            {
                Debug.Log("You win!");
                Application.Quit();
            }
            else
            {
                LoadRandom();
            }
        }
    }

    public void LoadRandom()
    {
        int random = Random.Range(0, scenes.Count);
        LoadScene(scenes[random]);
    }

    void LoadScene(SceneField scene)
    {
        Debug.Log("Loading level: " + scene.SceneName);

        // Load the selected scene, by scene name.
        currentScene = scene;
        SceneManager.LoadScene(currentScene.SceneName);
    }

    // Update is called every frame.
    void Update()
    {
    }
}
