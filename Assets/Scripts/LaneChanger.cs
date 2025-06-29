using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class LaneChanger : MonoBehaviour
{
    // Declare _omega, _dampingCoef for SpringUtils class spring motion movement
    // Declare _Spring action, an object to hold 4 coefficients for the SpringUtils class calculations
    [SerializeField] private float _omega;
    [SerializeField] private float _dampingCoef;
    private SpringUtils.tDampedSpringMotionParams _SpringAction = new SpringUtils.tDampedSpringMotionParams();

    // Declare _laneGap and _negLaneGap so the gap is configurable in Unity
    [SerializeField] public int _laneGap;
    private int _negLaneGap;

    // Declare flags moving left or right on key press by player, and declare bump left or bump right on instances where player tries
    // to move left at the left most lane and tries to move right on the right most lane
    private bool _isMovingLeft;
    private bool _isMovingRight;
    private bool _isBumpLeft;
    private bool _isBumpRight;

    // Declare floats _laneVelocity and _lanePosition for spring position and velocity SpringUtils Calculation.
    // Declare floats _leftBumpVelocity and _rightBumpVelocity for bump left and bump right
    private float _laneVelocity;
    private float _lanePosition;
    [SerializeField] private float _leftBumpVelocity;
    [SerializeField] private float _rightBumpVelocity;

    // Declare int for current lanes and desired lanes
    private int _desiredLane;
    private int _currentLane;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _laneVelocity = 0f;
        _negLaneGap = _laneGap * -1;
        _currentLane = 1;   // _currentLane will hold 0, 1, and 2 for left, middle, and right lane.
        _isBumpLeft = false;
        _isBumpRight = false;
    }

    private void OnMoveLeft()
    {
        // Checks if it's okay to move left by checking if your current lane is not on the most left position
        if (RealLanePositionConverter(_currentLane, _laneGap, _negLaneGap) > _negLaneGap)
        {

            // Immediately cancels flag for bump/moving the opposite direction, and in extension stops the spring motion position calculation
            _isBumpRight = false;
            _isBumpLeft = false;
            _isMovingRight = false;
            _laneVelocity = 0;

            // Calculate the SpringUtils class coefficients for spring motion on key press.
            SpringUtils.CalcDampedSpringMotionParams(
                ref _SpringAction,
                Time.deltaTime,
                _omega,
                _dampingCoef
            );

            // Records current lane position at this time
            _lanePosition = transform.position.x;
            _desiredLane = RealLanePositionConverter(_currentLane, _laneGap, _negLaneGap) + _negLaneGap;

            Debug.Log("_lanePosition recorded on OnMoveLeft() " + _lanePosition);
            Debug.Log("_currentLane recorded on OnMoveLeft() " + _currentLane);
            Debug.Log("_desiredLane recorded on OnMoveLeft() " + _desiredLane);

            _currentLane -= 1;

            _isMovingLeft = true;
        }

        // else when player is on the very left lane, will do a "bump" for feedback to player saying, hey you tried to move but
        // it's not actually allowed.
        else
        {
            // Immediately cancels bump/flag for moving in the opposite direction, and in extension stops the spring motion position calculation
            _isBumpRight = false;
            _isMovingLeft = false;
            _isMovingRight = false;
            _laneVelocity = _leftBumpVelocity;

            // Calculate the SpringUtils class coefficients for spring motion on key press.
            SpringUtils.CalcDampedSpringMotionParams(
                ref _SpringAction,
                Time.deltaTime,
                _omega,
                _dampingCoef
            );

            // Records current lane position at this time
            _lanePosition = transform.position.x;

            _isBumpLeft = true;

        }
        
    }

    private void OnMoveRight()
    {

        // Checks if it's okay to move right by checking if your current lane is not on the most right position
        if (RealLanePositionConverter(_currentLane, _laneGap, _negLaneGap) < _laneGap)
        {
            // Immediately cancels flag for bump/ moving in the opposite direction, and in extension stops the spring motion position calculation
            _isBumpLeft = false;
            _isBumpRight = false;
            _isMovingLeft = false;
            _laneVelocity = 0;

            // Calculate the SpringUtils class coefficients for spring motion on key press
            SpringUtils.CalcDampedSpringMotionParams(
                ref _SpringAction,
                Time.deltaTime,
                _omega,
                _dampingCoef
            );

            _lanePosition = transform.position.x;
            _desiredLane = RealLanePositionConverter(_currentLane, _laneGap, _negLaneGap) + _laneGap;


            Debug.Log("_lanePosition recorded on OnMoveLeft() " + _lanePosition);
            Debug.Log("_currentLane recorded on OnMoveLeft() " + _currentLane);
            Debug.Log("_desiredLane recorded on OnMoveLeft() " + _desiredLane);

            _currentLane += 1;

            _isMovingRight = true;

        }
        // else when player is on the very right lane, will do a "bump" for feedback to player saying, hey you tried to move but
        // it's not actually allowed.
        else
        {

            Debug.Log("Bump Right selected");

            // Immediately cancels flag for bump/ moving in the opposite direction, and in extension stops the spring motion position calculation
            _isBumpLeft = false;
            _isMovingLeft = false;
            _isMovingRight = false;
            _laneVelocity = _rightBumpVelocity;

            // Calculate the SpringUtils class coefficients for spring motion on key press.
            SpringUtils.CalcDampedSpringMotionParams(
                ref _SpringAction,
                Time.deltaTime,
                _omega,
                _dampingCoef
            );

            // Records current lane position at this time
            _lanePosition = transform.position.x;

            _isBumpRight = true;

            Debug.Log("_isBumpRight" + _isBumpRight);
        }
    }

    // Method converts Current Lane (is either 0,1,2) to actual lane position left, middle, right (NegLaneGap, 0, LaneGap depending on 
    // how wide the gap is set in Unity.
    // Can be rewritten as a list where 0,1,2 are the indexes
    private int RealLanePositionConverter(int CurrentLane, int LaneGap, int NegLaneGap)
    {
        if (CurrentLane == 0)
        {
            return NegLaneGap;
        }
        else if (CurrentLane == 1)
        {
            return 0;
        }
        else // if CurrentLane == 2 
        {
            return LaneGap;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("_laneGap is " + _laneGap);
        //Debug.Log("_negLaneGap is " + _negLaneGap);


        if (_isMovingLeft == true)
        {
            // Calculate next position using damped spring code
            SpringUtils.UpdateDampedSpringMotion(
                    ref _lanePosition,
                    ref _laneVelocity,
                    _desiredLane,
                    _SpringAction
                );

            // Apply the new calculated position using transform.position. Keeps current y and z positions.
            transform.position = new Vector3(_lanePosition, transform.position.y, transform.position.z);

            // Checks if the velocity calculated in the damped spring code is essentially 0, meaning the spring is at rest.
            // If so, then it stops the damped spring code and teleports the GameObject exactly at the lane position.
            if (Math.Abs(_laneVelocity) < 0.01 && Math.Abs(_lanePosition - _desiredLane) < 0.01)
            {
                Debug.Log("Left Move - Spring Update position calc finished");
                _isMovingLeft = false;
                transform.position = new Vector3(_desiredLane, transform.position.y, transform.position.z);
            }
        }

        else if (_isBumpLeft == true)
        {
            // Calculate next position using damped spring code
            SpringUtils.UpdateDampedSpringMotion(
                    ref _lanePosition,
                    ref _laneVelocity,
                    _desiredLane,
                    _SpringAction
                );

            // Apply the new calculated position using transform.position. Keeps current y and z positions.
            transform.position = new Vector3(_lanePosition, transform.position.y, transform.position.z);

            // Checks if the velocity calculated in the damped spring code is essentially, meaning the spring is at rest.
            // If so, then it stops the damped spring code and teleports the GameObject exactly at the lane position.
            if (Math.Abs(_laneVelocity) < 0.01 && Math.Abs(_lanePosition - _desiredLane) < 0.01)
            {
                Debug.Log("Left Bump - Spring Update position calc finished");
                _isBumpLeft = false;
                transform.position = new Vector3(_desiredLane, transform.position.y, transform.position.z);
            }

        }

        else if (_isMovingRight == true)
        {

            // Calculate next position using damped spring code
            SpringUtils.UpdateDampedSpringMotion(
                ref _lanePosition,
                ref _laneVelocity,
                _desiredLane,
                _SpringAction
            );

            // Apply the new calculated position using transform.position. Keeps current y and z positions.
            transform.position = new Vector3(_lanePosition, transform.position.y, transform.position.z);

            // Checks if the velocity calculated in the damped spring code is essentially, meaning the spring is at rest.
            // If so, then it stops the damped spring code and teleports the GameObject exactly at the lane position.
            if (Math.Abs(_laneVelocity) < 0.01 && Math.Abs(_lanePosition - _desiredLane) < 0.01)
            {
                Debug.Log("Right Move - Spring Update position calc finished");
                _isMovingRight = false;
                transform.position = new Vector3(_desiredLane, transform.position.y, transform.position.z);
            }
        }

        else if (_isBumpRight == true)
        {
            Debug.Log("Doing bump right spring calc");

            // Calculate next position using damped spring code
            SpringUtils.UpdateDampedSpringMotion(
                ref _lanePosition,
                ref _laneVelocity,
                _desiredLane,
                _SpringAction
            );

            // Apply the new calculated position using transform.position. Keeps current y and z positions.
            transform.position = new Vector3(_lanePosition, transform.position.y, transform.position.z);

            // Checks if the velocity calculated in the damped spring code is essentially, meaning the spring is at rest.
            // If so, then it stops the damped spring code and teleports the GameObject exactly at the lane position.
            if (Math.Abs(_laneVelocity) < 0.01 && Math.Abs(_lanePosition - _desiredLane) < 0.01)
            {
                Debug.Log("Right Bump - Spring Update position calc finished");
                _isBumpRight = false;
                transform.position = new Vector3(_desiredLane, transform.position.y, transform.position.z);
            }
        }
    }
}
