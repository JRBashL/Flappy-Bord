using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LaneStateMachine", menuName = "Scriptable Objects/LaneStateMachine")]
public class LaneStateMachineSO : ScriptableObject
{


    private LaneChangeFSM _laneChangeState;
    private CurrentLaneFSM _currentLaneState;
    public enum LaneChangeFSM
    {
        OnLane,
        LaneChangeLeft,
        LaneBumpLeft,
        LaneChangeRight,
        LaneBumpRight
    }

    public enum CurrentLaneFSM
    {
        Left,
        Center,
        Right
    }

    //Property
    public LaneChangeFSM LaneChangeState
    {
        get
        {
            return _laneChangeState;
        }

        set
        {
            if (!Enum.IsDefined(typeof(LaneChangeFSM), value))
            {
                Debug.LogError("Setting wrong value for LaneChangeFSM. Setting to Onlane.");
                _laneChangeState = LaneChangeFSM.OnLane;
            }

            else
            {
                _laneChangeState = value;
            }
        }
    }

    public CurrentLaneFSM CurrentLaneState
    {
        get => _currentLaneState;

        set
        {
            if (!Enum.IsDefined(typeof(CurrentLaneFSM), value))
            {
                Debug.LogError("Setting wrong value for CurrentLaneFSM. Setting to Center.");
                _currentLaneState = CurrentLaneFSM.Center;
            }

            else
            {
                _currentLaneState = value;
            }
        }
    }

    // Set default starting value.
    void OnEnable()
    {
        _laneChangeState = LaneChangeFSM.OnLane;
        _currentLaneState = CurrentLaneFSM.Center;
    }

}
