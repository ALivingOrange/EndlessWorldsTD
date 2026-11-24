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

    private void Start()
    {
        Debug.Log("GameManagerTester has started!");

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance not found!");
            return;
        }

        GameManager.Instance.OnGameStateChanged += LogStateChange;
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


    }
    private void LogStateChange(GameState newState)
    {
        Debug.Log("TEST: Game state is now " + newState);
    }



}