using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LaneStateMachine", menuName = "Scriptable Objects/LaneStateMachine")]
public class LaneStateMachine : ScriptableObject
{

    [SerializeField]
    private LaneChangeFSM _laneChangeState;
    public enum LaneChangeFSM
    {
        OnLane,
        LaneChangeLeft,
        LaneBumpLeft,
        LaneChangeRight,
        LaneBumpRight
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

    // Set default starting value.
    void OEnable()
    {
        _laneChangeState = LaneChangeFSM.OnLane;
    }

}
