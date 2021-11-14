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

    // Start game button triggers this
    public void StartGame()
    {
        GetNextLevel();
    }

    // Informs GameManager that the level has been completed
    // Game manager should then kick off the orchestration sequence to load teh next known level
    public void LevelComplete(LevelManager level, bool won)
    {
        Debug.Log("Current Scene has finished");

        // If the finishing scene is a Game Level, then we need to update some state
        if (levels.IndexOf(CurrentScene) != -1)
        {
            // If the level was won, then we need to add it to the completed levels list
            Debug.Log("Current");
            this.score += score;
            completedLevels.Add (CurrentScene);
            levels.Remove (CurrentScene);
        }

        // Sequence of events to load the next level
        level.TearDown();
    }

    public void LevelUnloaded(LevelManager level)
    {
        GetNextLevel();
    }

    public void GetNextLevel()
    {
        if (levels.Count > 0)
        {
            Debug.Log("Loading random level");
            int index = Random.Range(0, levels.Count);
            StartCoroutine(LoadLevel(levels[index]));
        }
        else
        {
            Debug.Log("No more levels, returning to menu");

            // Add completed levels to the list of levels to be loaded.
            levels.AddRange (completedLevels); //TODO remove this
            completedLevels.Clear();
            LoadScene (CompleteScene);
        }
    }

    public IEnumerator LoadLevel(SceneField scene)
    {
        Debug.Log("Loading level");
        CurrentScene = scene;

        // Load level async
        yield return SceneManager.LoadSceneAsync(scene.SceneName);

        // Get the level manager
        CurrentLevel = GameObject.FindObjectOfType<LevelManager>();
        CurrentLevel.gameManager = this;
    }

    public void LevelLoaded(LevelManager level)
    {
        CurrentLevel = level;
        CurrentLevel.gameManager = this;
        CurrentLevel.Setup();
    }

    void LoadScene(SceneField scene)
    {
        Debug.Log("Loading Scene: " + scene.SceneName);
        SceneManager.LoadScene(scene.SceneName);
    }

    public void LevelReady(LevelManager level)
    {
        level.Play();
    }
}
