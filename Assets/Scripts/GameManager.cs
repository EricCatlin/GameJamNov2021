using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // Create an array of levels to be loaded
    public List<SceneField> levels;

    public List<SceneField> completedLevels;

    public SceneField currentScene;

    public int score = 0;

    // Store a reference to our levelScript which will set up the level.
    private LevelManager CurrentLevel;

    public SceneField MenuScene;

    public SceneField CompleteScene;

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

    public static GameManager GetInstance()
    {
        return instance;
    }

    // Initializes the game for each level.
    void InitGame()
    {
        // Call the SetupScene function of the levelScript script, pass it current level number.
        CurrentLevel.Begin();
    }

    public void Complete(int score = 1)
    {
        if (CurrentLevel != null)
        {
            this.score += score;
            completedLevels.Add (currentScene);
            LoadRandomLevel();
        }
    }

    public void LoadRandomLevel()
    {
        if (levels.Count > 0)
        {
            int index = Random.Range(0, levels.Count);
            LoadLevel(levels[index]);
            return;
        }
        else
        {
            LoadScene (CompleteScene);
        }
    }

    void LoadScene(SceneField scene)
    {
        Debug.Log("Loading Scene: " + scene.SceneName);

        // Load the selected scene, by scene name.
        SceneManager.LoadScene(currentScene.SceneName);
    }

    void LoadLevel(SceneField scene)
    {
        Debug.Log("Loading level: " + scene.SceneName);

        // Load the selected scene, by scene name.
        currentScene = scene;
        levels.Remove (scene);
        SceneManager.LoadScene(currentScene.SceneName);
    }

    // Update is called every frame.
    void Update()
    {
    }
}
