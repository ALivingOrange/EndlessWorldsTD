/***************************************************************
*file: GameManager.cs
*author: Octavio Munoz
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/20/2026
*
*purpose: Coordinator responsible for tracking shared states and values which many other scripts are dependent upon. 
*         Always loaded regardless of active scenes (in its own persistent scene)
****************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

// enum: GameState
// purpose: Represents the states various the game can be in.
public enum GameState
{
    MainMenu,
    Loading,
    Play,
    Paused,
    GameOver
}
public class GameManager : MonoBehaviour
{


    // variable: Instance
    // purpose: Provides access to the one persistent GameManager
    // instance used throughout the game.
    public static GameManager Instance { get; private set; }



    // variable: CurrentState
    // purpose: Stores the current state of the game.
    public GameState CurrentState { get; private set; }



    // function: Awake
    // purpose: Ensures that only one GameManager exists and keeps it
    // active when Unity changes scenes.
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    // function: Start
    // purpose: Sets initial game state
    private void Start()
    {
        CurrentState = GameState.MainMenu;
    }



    // function: ChangeState
    // purpose: Changes the current state of the game.
    private void ChangeState(GameState newState)
    {
        CurrentState = newState;
    }



    // function: LoadMainMenu
    // purpose: Loads the main menu scene.
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        ChangeState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }



    // function: StartGame
    // purpose: Loads the gameplay scene.
    public void StartGame()
    {
        Time.timeScale = 1f;
        ChangeState(GameState.Loading);
        SceneManager.LoadScene("SampleScene");

        // Temporary until proper loading is implemented.
        ChangeState(GameState.Play);
    }



    // function: PauseGame
    // purpose: Pauses gameplay.
    public void PauseGame()
    {
        if (CurrentState == GameState.Play)
        {
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
        }
    }



    // function: ResumeGame
    // purpose: Resumes gameplay.
    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            Time.timeScale = 1f;
            ChangeState(GameState.Play);
        }
    }



    // function: EndGame
    // purpose: Changes the game to the GameOver state.
    public void EndGame()
    {
        Time.timeScale = 1f;
        ChangeState(GameState.GameOver);
    }


}