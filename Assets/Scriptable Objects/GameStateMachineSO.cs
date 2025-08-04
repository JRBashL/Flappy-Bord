using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameStateMachineSO", menuName = "Scriptable Objects/GameStateMachineSO")]
public class GameStateMachineSO : ScriptableObject
{
    public enum GameFSM
    {
        GameStart,
        Normal,
        Haste,
        GameOver,
    }

    private GameFSM _gameState;

    // Property
    public GameFSM GameState
    {
        get => _gameState;
        set
        {
            if (!Enum.IsDefined(typeof(GameFSM), value))
            {
                Debug.LogError("Setting wrong value for GameFSM. Setting to Normal");
                _gameState = GameFSM.Normal;
            }

            else
            {
                _gameState = value;
            }
        }
    }

    // Methods for Events
    public void StartGameStartState()
    {
        GameState = GameFSM.GameStart;
    }
    public void StartNormalState()
    {
        GameState = GameFSM.Normal;
    }
    public void StartHasteState()
    {
        GameState = GameFSM.Haste;
    }
    public void StartGameOverState()
    {
        GameState = GameFSM.GameOver;
    }
}   
