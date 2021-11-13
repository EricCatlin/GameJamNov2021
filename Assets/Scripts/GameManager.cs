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

    public SceneField CurrentScene;

    public int score = 0;

    // Store a reference to our levelScript which will set up the level.
    public LevelManager CurrentLevel;

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
        if (CurrentScene == null)
        {
            LoadScene (MenuScene);
        }
    }

    public void LevelFinished(int score = 1)
    {
        Debug.Log("Complete");
        if (CurrentLevel != null)
        {
            Debug.Log("Current");
            this.score += score;
            completedLevels.Add (CurrentScene);
        }
        LoadRandomLevel();
    }

    public void LoadRandomLevel()
    {
        if (levels.Count > 0)
        {
            Debug.Log("Loading random level");
            int index = Random.Range(0, levels.Count);
            LoadLevel(levels[index]);
            return;
        }
        else
        {
            Debug.Log("Loading Complete");

            // Add completed levels to the list of levels to be loaded.
            levels.AddRange (completedLevels); //TODO remove this
            completedLevels.Clear();
            LoadScene (CompleteScene);
        }
    }

    void LoadScene(SceneField scene)
    {
        Debug.Log("Loading Scene: " + scene.SceneName);
        SceneManager.LoadScene(scene.SceneName);
    }

    void LoadLevel(SceneField scene)
    {
        Debug.Log("Loading level: " + scene.SceneName);
        levels.Remove (scene);
        SceneManager.LoadScene(scene.SceneName);
        CurrentScene = scene;
    }

    public void StartGame()
    {
        LoadRandomLevel();
    }

    public void LevelLoaded(LevelManager level)
    {
        CurrentLevel = level;
        level.Begin();
    }

    // Update is called every frame.
    void Update()
    {
    }
}
