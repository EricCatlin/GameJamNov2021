using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // Create an array of levels to be loaded
    public List<SceneField> levels;

    public List<SceneField> playlist;

    public List<SceneField> completedLevels;

    public SceneField CurrentScene;

    public int score = 0;

    public int maxScore = 0;

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
            StartCoroutine(LoadLevel(MenuScene));
        }
    }

    // Start game button triggers this
    public void StartGame()
    {
        score = 0;
        maxScore = 0;

        // Shuffle levels and select n
        playlist = levels.OrderBy(a => System.Guid.NewGuid()).Take(2).ToList();
        Debug.Log("Playlist: " + playlist.Count);
        GetNextLevel();
    }

    // Informs GameManager that the level has been completed
    // Game manager should then kick off the orchestration sequence to load teh next known level
    public void LevelComplete(
        LevelManager level,
        int score = 0,
        int maxScore = 0
    )
    {
        Debug.Log("Current Scene has finished");

        // If the finishing scene is a Game Level, then we need to update some state
        if (playlist.IndexOf(CurrentScene) != -1)
        {
            // If the level was won, then we need to add it to the completed levels list
            Debug.Log("Current");
            this.score += score;
            this.maxScore += maxScore;
            completedLevels.Add (CurrentScene);
            playlist.Remove (CurrentScene);
        }

        // Sequence of events to load the next level
        level.TearDown();
    }

    public void GoToMenu()
    {
        CurrentScene = null;

        if (completedLevels.Count > 0)
        {
            playlist.Clear();
            completedLevels.Clear();
        }
        StartCoroutine(LoadLevel(MenuScene));
    }

    public void LevelUnloaded(LevelManager level)
    {
        GetNextLevel();
    }

    public void GetNextLevel()
    {
        if (playlist.Count > 0)
        {
            Debug.Log("Loading random level");
            StartCoroutine(LoadLevel(playlist[0]));
        }
        else
        {
            Debug.Log("No more levels, returning to menu");
            StartCoroutine(LoadLevel(CompleteScene));
        }
    }

    public IEnumerator LoadLevel(SceneField scene)
    {
        Debug.Log("Loading level");

        // Load level async
        yield return SceneManager.LoadSceneAsync(scene.SceneName);
        CurrentScene = scene;

        // Get the level manager
        CurrentLevel = GameObject.FindObjectOfType<LevelManager>();
        CurrentLevel.gameManager = this;
    }

    public void LevelLoaded(LevelManager level)
    {
        Debug.Log("Level Loaded");
        CurrentLevel = level;
        if (CurrentLevel.gameManager == null) CurrentLevel.gameManager = this;
        CurrentLevel.Setup();
    }

    public void LevelReady(LevelManager level)
    {
        level.Play();
    }
}
