using UnityEngine;

[CreateAssetMenu(fileName = "GameStateMachineSO", menuName = "Scriptable Objects/GameStateMachineSO")]
public class GameStateMachineSO : ScriptableObject
{
    public enum GameFSM
    {
        Begin,
        Regular,
        SpeedBoost,
        GameOver,
    }

    
}
