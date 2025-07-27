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

    [SerializeField]
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

    void OnEnable()
    {
        _pipeSpawnState = PipeSpawnFSM.NoSpawn;
    }

    // State Change Methods for Events
    public void StartNoSpawnState()
    {
        PipeSpawnState = PipeSpawnFSM.NoSpawn;
    }

    public void StartRegularSpawnState()
    {
        PipeSpawnState = PipeSpawnFSM.RegularSpeedSpawn;
    }

    public void StartSpeedBoostSpawnState()
    {
        PipeSpawnState = PipeSpawnFSM.SpeedBoostSpawn;
    }
}
