using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PipeSpeedStateSO", menuName = "Scriptable Objects/PipeSpeedStateSO")]
public class PipeSpeedStateSO : ScriptableObject
{
    public enum PipeSpeedFSM
    {
        RegularPipeSpeed,
        BoostedPipeSpeed,
        DecelPipeSpeed,
        StopPipeSpeed
    };

    private PipeSpeedFSM _pipeSpeedState;

    // Property
    public PipeSpeedFSM PipeSpeedState
    {
        get => _pipeSpeedState;

        set
        {
            if (!Enum.IsDefined(typeof(PipeSpeedFSM), value))
            {
                Debug.LogError("Setting wrong value for PipeSpeedFSM. Setting to RegularPipeSpeed");
                _pipeSpeedState = PipeSpeedFSM.RegularPipeSpeed;
            }

            else
            {
                _pipeSpeedState = value;
            }

        }

    }

    void OnEnable()
    {
        PipeSpeedState = PipeSpeedFSM.RegularPipeSpeed;
    }

    // Methods for Events
    public void StartRegularSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.RegularPipeSpeed;
    }

    public void StartBoostSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.BoostedPipeSpeed;
    }

    public void StartDecelSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.DecelPipeSpeed;
    }
    
    public void StarStopSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.StopPipeSpeed;
    }

}
