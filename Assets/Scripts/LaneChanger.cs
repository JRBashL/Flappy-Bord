using System;
using UnityEngine;
// Shorthanding the external script for the enums
using LaneChangeSOFSM = LaneStateMachine.LaneChangeFSM;
using LaneStateSOFSM = LaneStateMachine.CurrentLaneFSM;
using BordStateSOFSM = BordStateMachine.BordStateEnum;

public class LaneChanger : MonoBehaviour
{
    #region SpringUtils

        // Declare _omega, _dampingCoef for SpringUtils class spring motion movement
        // Declare _Spring action, an object to hold 4 coefficients for the SpringUtils class calculations
        [Header("Spring Properties")]
        [SerializeField] private float _omega, _dampingCoef;
        private SpringUtils.tDampedSpringMotionParams _springAction = new SpringUtils.tDampedSpringMotionParams();

    #endregion

    #region Lane Variables

    // Declare _laneGap so the gap is configurable in Unity. Stores in an array
    // Declare floats _laneVelocity and _lanePosition for spring position and velocity SpringUtils Calculation.
    // Declare floats _leftBumpVelocity and _rightBumpVelocity for bump left and bump right
    // Declare int for current lanes and desired lanes
        [Header("Lane Change Properties")]
        [SerializeField] public int _laneGap;
        int[] _laneCoord;
        private float _laneVelocity, _lanePosition;
        [SerializeField] private float _leftBumpVelocity, _rightBumpVelocity;
        private int _desiredLane;

    #endregion

    #region FSM
    
        [Header("FSM Scriptable Objects")]
        // Get reference to LaneStateMachine and BordStateMachine
        [SerializeField] private LaneStateMachine _laneStateMachineSO;
        [SerializeField] private BordStateMachine _bordStateMachineSO;

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _laneVelocity = 0f;
        _laneCoord = new int[]{ _laneGap * -1, 0, _laneGap };
    }

    void Update()
    {
        if (_bordStateMachineSO.CurrentBordState == BordStateSOFSM.BeginState || _bordStateMachineSO.CurrentBordState == BordStateSOFSM.DeadState)
        {
            // Does not do anything if Bord is Dead.
        }

        // Peforms this code if the lange change state is anything but OnLane, meaning when it's in the middle of changing lanes.
        else if (_laneStateMachineSO.LaneChangeState != LaneChangeSOFSM.OnLane)
        {
            // Calculate next position using damped spring code
            SpringUtils.UpdateDampedSpringMotion(ref _lanePosition, ref _laneVelocity, _desiredLane, _springAction);

            switch (_laneStateMachineSO.LaneChangeState)
            {
                case LaneChangeSOFSM.LaneChangeLeft:

                    // Apply the new calculated position using transform.position. Keeps current y and z positions.
                    transform.position = new Vector3(_lanePosition, transform.position.y, transform.position.z);
                    LaneChangeEnder();
                    break;

                case LaneChangeSOFSM.LaneBumpLeft:

                    // Apply the new calculated position using transform.position. Keeps current y and z positions.
                    transform.position = new Vector3(_lanePosition, transform.position.y, transform.position.z);
                    LaneChangeEnder();
                    break;

                case LaneChangeSOFSM.LaneChangeRight:

                    // Apply the new calculated position using transform.position. Keeps current y and z positions.
                    transform.position = new Vector3(_lanePosition, transform.position.y, transform.position.z);
                    LaneChangeEnder();
                    break;

                case LaneChangeSOFSM.LaneBumpRight:

                    // Apply the new calculated position using transform.position. Keeps current y and z positions.
                    transform.position = new Vector3(_lanePosition, transform.position.y, transform.position.z);
                    LaneChangeEnder();
                    break;
            }
        }
    }

    private void OnMoveLeft()
    {
        if (_bordStateMachineSO.CurrentBordState == BordStateSOFSM.DeadState || _bordStateMachineSO.CurrentBordState == BordStateSOFSM.DeadState)
        {
            // Does nothing in the states above.
        }

        else
        {
            // Calculate the SpringUtils class coefficients for spring motion on key press.
            SpringUtils.CalcDampedSpringMotionParams(ref _springAction, Time.deltaTime, _omega, _dampingCoef);

            // Checks if it's okay to move left by checking if your current lane is not on the most left position
            if (_laneStateMachineSO.CurrentLaneState != LaneStateSOFSM.Left)
            {
                // Reset lane velocity
                _laneVelocity = 0;

                // Records current lane position at this time and desired lane for spring calculation.
                _lanePosition = transform.position.x;
                _desiredLane = RealLanePositionConverter() - _laneGap;

                // State changes. For the Currentlane state, it shifts the current enum one to the left.
                _laneStateMachineSO.LaneChangeState = LaneChangeSOFSM.LaneChangeLeft;
                _laneStateMachineSO.CurrentLaneState = (LaneStateSOFSM)((int)_laneStateMachineSO.CurrentLaneState - 1);
            }

            // When player is on the very left lane, will do a "bump" for feedback to player saying, hey you tried to move but it's not allowed.
            else
            {
                // Records current lane position and sets velocity to bump velocity for spring calculation.
                _lanePosition = transform.position.x;
                _laneVelocity = _leftBumpVelocity;

                // State change
                _laneStateMachineSO.LaneChangeState = LaneChangeSOFSM.LaneBumpLeft;
            }
        }
    }

    private void OnMoveRight()
    {
        if (_bordStateMachineSO.CurrentBordState == BordStateSOFSM.DeadState || _bordStateMachineSO.CurrentBordState == BordStateSOFSM.DeadState)
        {
            // Does nothing in the states above.
        }

        else
        {
            // Calculate the SpringUtils class coefficients for spring motion on key press.
            SpringUtils.CalcDampedSpringMotionParams(ref _springAction, Time.deltaTime, _omega, _dampingCoef);

            // Checks if it's okay to move right by checking if your current lane is not on the most right position
            if (_laneStateMachineSO.CurrentLaneState != LaneStateSOFSM.Right)
            {
                // Reset lane velocity
                _laneVelocity = 0;

                // Records current lane position at this time and desired lane for spring calculation.
                _lanePosition = transform.position.x;
                _desiredLane = RealLanePositionConverter() + _laneGap;

                // State changes. For the Currentlane state, it shifts the current enum one to the left.
                _laneStateMachineSO.LaneChangeState = LaneChangeSOFSM.LaneChangeRight;
                _laneStateMachineSO.CurrentLaneState = (LaneStateSOFSM)((int)_laneStateMachineSO.CurrentLaneState + 1);
            }

            // When player is on the very right lane, will do a "bump" for feedback to player saying, hey you tried to move but it's not allowed.
            else
            {
                // Records current lane position and sets velocity to bump velocity for spring calculation.
                _lanePosition = transform.position.x;
                _laneVelocity = _rightBumpVelocity;

                // State changes
                _laneStateMachineSO.LaneChangeState = LaneChangeSOFSM.LaneBumpRight;

            }
        }
    }

    // Method converts CurrentLaneFSM (is either center, left, right) to actual lane coordinates (NegLaneGap, 0, LaneGap ) depending on 
    // how wide the gap is set in Unity.
    private int RealLanePositionConverter()
    {
        switch (_laneStateMachineSO.CurrentLaneState)
        {
            case LaneStateSOFSM.Left:
                return _laneCoord[0];
            case LaneStateSOFSM.Center:
                return _laneCoord[1];
            case LaneStateSOFSM.Right:
                return _laneCoord[2];
            default: return 0;
        }
    }

    // Checks if the velocity calculated in the damped spring code is essentially 0, meaning the spring is at rest.
    // If so, then it stops the damped spring code and teleports the GameObject exactly at the lane position.
    private void LaneChangeEnder()
    {
        if (Math.Abs(_laneVelocity) < 0.01 && Math.Abs(_lanePosition - _desiredLane) < 0.01)
        {
            _laneStateMachineSO.LaneChangeState = LaneChangeSOFSM.OnLane;
            transform.position = new Vector3(_desiredLane, transform.position.y, transform.position.z);
        }
    }
}
