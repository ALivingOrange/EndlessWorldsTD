/***************************************************************
*file: GameManager.cs
*author: Octavio Munoz
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/20/2026
*
*purpose: Coordinator responsible for tracking shared states and values that many other scripts rely on. 
*         Always loaded regardless of active scenes (in its own persistent scene)
****************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


// enum: GameState
// purpose: The various states the game can be in.
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
    // purpose: Provides access to the GameManager instance.
    public static GameManager Instance { get; private set; }



    // variable: CurrentState
    // purpose: Stores current GameState.
    public GameState CurrentState { get; private set; }


    // event: OnGameStateChanged
    // purpose: Notifies other systems when the game state changes.
    public event System.Action<GameState> OnGameStateChanged;



    // variable: gameOverCoroutine
    // purpose: Tracks the delayed Game Over coroutine to stop it when changing scenes
    private Coroutine gameOverCoroutine;




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
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        Debug.Log("Game state changed to: " + CurrentState);

        OnGameStateChanged?.Invoke(CurrentState);
    }

#if UNITY_EDITOR
    public void TestEnterPlayState()
    {
        ChangeState(GameState.Play);
    }
#endif

    // function: LoadMainMenu
    // purpose: Loads the main menu scene.
    public void LoadMainMenu()
    {
        CancelDelayedGameOver();

        Time.timeScale = 1f;
        ChangeState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }



    // function: StartGame
    // purpose: Loads the gameplay scene.
    public void StartGame()
    {
        CancelDelayedGameOver();

        Time.timeScale = 1f;
        ChangeState(GameState.Loading);
        SceneManager.LoadScene("SampleScene");

        // Temporary until proper loading is implemented.
        ChangeState(GameState.Play);
    }



    // function: RestartGame
    // purpose: Restarts the current gameplay session.
    public void RestartGame()
    {
        CancelDelayedGameOver();

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
    // purpose: Switches game to GameOver state.
    public void EndGame()
    {
        if (CurrentState == GameState.GameOver)
            return;

        Time.timeScale = 1f;
        ChangeState(GameState.GameOver);

        gameOverCoroutine = StartCoroutine(DelayedGameOver());
    }



    // function: DelayedGameOver
    // purpose: Delays the gameover game pause for a few seconds.
    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(3f);

        Time.timeScale = 0f;

    }


    // function: CancelDelayedGameOver
    // purpose: Stops the delayed Game Over freeze when the game changes scenes
    private void CancelDelayedGameOver()
    {
        if (gameOverCoroutine != null)
        {
            StopCoroutine(gameOverCoroutine);
            gameOverCoroutine = null;

        }
    }


}