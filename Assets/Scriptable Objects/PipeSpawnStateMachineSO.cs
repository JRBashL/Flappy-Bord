using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PipeSpawnStateMachineSO", menuName = "Scriptable Objects/PipeSpawnStateMachineSO")]
public class PipeSpawnStateMachineSO : ScriptableObject
{
    public enum PipeSpawnFSM
    {
        NoSpawn,
        RegularSpeedSpawn,
        SpeedBoostSpawn
    }

    private PipeSpawnFSM _pipeSpawnState;

    public PipeSpawnFSM PipeSpawnState
    {
        get
        {
            return _pipeSpawnState;
        }

        set
        {
            if (!Enum.IsDefined(typeof(PipeSpawnFSM), value))
            {
                Debug.LogError("Setting wrong value for PipeSpawnFSM. Setting to RegularSpeedSpawn.");
                _pipeSpawnState = PipeSpawnFSM.RegularSpeedSpawn;
            }

            else
            {
                _pipeSpawnState = value;
            }
        }
    }
}
