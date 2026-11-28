/***************************************************************
*file: NewEmptyCSharpScript.cs
*author: (FILL THIS IN!)
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/23/2026
*
*purpose: (FILL THIS IN!)
*
****************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

// class: GameManagerTester
// purpose: Tests GameManager functionality without other game systems.
public class GameManagerTester : MonoBehaviour
{

    [SerializeField]
    private Resource wood;

    [SerializeField]
    private Resource ore;



    private void Start()
    {
        Debug.Log("GameManagerTester has started!");

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance not found!");
            return;
        }

        GameManager.Instance.OnGameStateChanged += LogStateChange;

        TestInventory();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= LogStateChange;
        }
    }



    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (Keyboard.current == null)
            return;

        // Press P to pause.
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Debug.Log("P pressed. Current state: " +
                      GameManager.Instance.CurrentState);

            GameManager.Instance.PauseGame();
        }

        // Press R to resume.
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Debug.Log("R pressed. Current state: " +
                      GameManager.Instance.CurrentState);

            GameManager.Instance.ResumeGame();
        }

        // Press G to simulate Game Over.
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            GameManager.Instance.EndGame();
        }

        // Press T to enter the Play state.
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            GameManager.Instance.TestEnterPlayState();
        }

        // Press F to transition scenes
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            TestSceneTransition();
        }

    }

    // log current state
    private void LogStateChange(GameState newState)
    {
        Debug.Log("TEST: Game state is now " + newState);
    }



    // function: TestInventory
    // purpose: Tests resource gains, resource spending, and insufficient resources.
    private void TestInventory()
    {
        if (wood == null || ore == null)
        {
            Debug.LogError("Inventory test resources are missing.");
            return;
        }

        GameManager.Instance.ResourceGain(wood, 20);
        GameManager.Instance.ResourceGain(ore, 15);

        Debug.Log("Wood after collecting: " +
            GameManager.Instance.GetResourceAmount(wood));

        Debug.Log("Ore after collecting: " +
            GameManager.Instance.GetResourceAmount(ore));

        bool firstSpend =
            GameManager.Instance.ResourceSpend(wood, 5);

        Debug.Log("Wood spend successful: " + firstSpend);

        Debug.Log("Wood remaining: " +
            GameManager.Instance.GetResourceAmount(wood));

        bool secondSpend =
            GameManager.Instance.ResourceSpend(ore, 20);

        Debug.Log("Ore spend successful: " + secondSpend);

        Debug.Log("Ore remaining: " +
            GameManager.Instance.GetResourceAmount(ore));
    }


    // function: TestSceneTransition
    // purpose: Tests whether GameManager persists between scenes.
    public void TestSceneTransition()
    {
        Debug.Log("GameManager before transition: " +
                  GameManager.Instance.GetEntityId());

        UnityEngine.SceneManagement.SceneManager.LoadScene("EnemyAITestWorld");
    }

}