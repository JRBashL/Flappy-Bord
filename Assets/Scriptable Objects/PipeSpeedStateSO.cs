using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PipeSpeedStateSO", menuName = "Scriptable Objects/PipeSpeedStateSO")]
public class PipeSpeedStateSO : ScriptableObject
{
    public enum PipeSpeedFSM
    {
        RegularPipeSpeed,
        AccelPipeSpeed,
        BoostedPipeSpeed,
        DecelPipeSpeed,
        StopPipeSpeed,
        ZeroPipeSpeed
    };

    [SerializeField]
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

    // Methods for Events
    public void StartRegularSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.RegularPipeSpeed;
    }

    public void StartAccelSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.AccelPipeSpeed;
    }

    public void StartBoostSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.BoostedPipeSpeed;
    }

    public void StartDecelSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.DecelPipeSpeed;
    }

    public void StartStopSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.StopPipeSpeed;
    }
    
    public void StartZeroSpeed()
    {
        PipeSpeedState = PipeSpeedFSM.ZeroPipeSpeed;
    }

}
