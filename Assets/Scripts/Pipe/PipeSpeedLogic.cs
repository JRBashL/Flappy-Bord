using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using UnityEngine.UIElements;

public class PipeSpeedLogic : MonoBehaviour
{

    // Declare the Scriptable Object that holds the data for the pipe speeds

    [SerializeField]
    [Tooltip("Place PipeSpeedScriptableObject here that contains the pipe speeds. Use it to configure regular and boosted speeds.")]
    private PipeScriptableObject _pipeSpeedSO;

    // Declare pipe speeds and durations
    private float _regularPipeSpeed, _boostPipeSpeedMultiplier, _boostDuration, _decelDuration, _currentPipeSpeed, _increaseSpeedPerSec;

    // Declare fallback speeds
    private const float _defaultPipeSpeed = -15f;
    private const float _defaultBoostPipeSpeedMultiplier = 3;
    private const float _defaultboostDuration = 5f;
    private const float _defaultDecelDuration = 2f;
    private const float _defaultIncreaseSpeedPerSec = 0.05f;

    // Declare Easing Functions
    private EaseFunc.Ease _easeEnum;
    private EaseFunc.Function _easeFunction;

    // Declare enum for state and couroutines
    private enum Speedstate { _enumRegularPipeSpeed, _enumBoostedPipeSpeed, _enumDecel, _enumStopSpeed };
    private Speedstate _currentSpeedState;
    private Coroutine _currentStateCoroutine;

    void Awake()
    {
        _regularPipeSpeed = _pipeSpeedSO.RegularPipeSpeed;
        _currentSpeedState = Speedstate._enumRegularPipeSpeed;

        // If statement to initialze the pipe speed fields. Has a fallback in case the scriptable object to be assigned in the inspector
        // isn't placed.
        if (_pipeSpeedSO == null)
        {
            Debug.LogWarning("PipeSpeedScriptableObject not assigned in inspector for PipeSpeedLogic!");
            Debug.LogWarning("Setting fallback values.");
            _regularPipeSpeed = _defaultPipeSpeed;
            _boostPipeSpeedMultiplier = _defaultBoostPipeSpeedMultiplier;
            _boostDuration = _defaultboostDuration;
            _decelDuration = _defaultDecelDuration;
            _increaseSpeedPerSec = _defaultIncreaseSpeedPerSec;
        }
        else
        {
            _regularPipeSpeed = _pipeSpeedSO.RegularPipeSpeed;
            _boostPipeSpeedMultiplier = _pipeSpeedSO.BoostPipeSpeedMultiplier;
            _boostDuration = _pipeSpeedSO.BoostDuration;
            _decelDuration = _pipeSpeedSO.DecelDuration;
            _increaseSpeedPerSec = _pipeSpeedSO.IncreasingSpeedPerSec;
        }

        _easeEnum = EaseFunc.Ease.EaseOutQuint;
        _easeFunction = EaseFunc.GetEasingFunction(_easeEnum);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StateChangerRegularSpeed();
    }

    // Update is called once per frame
    void Update()
    {

    }



    // Coroutine state on regular gameplay
    private IEnumerator RegularSpeedState()
    {
        Debug.Log("PipeSpeedLogic Regular Speed State Activated.");
        _currentSpeedState = Speedstate._enumRegularPipeSpeed;

        PipeScript.PipeSpeed = _regularPipeSpeed;

        // Speed constantly increases
        do
        {
            _regularPipeSpeed += _increaseSpeedPerSec;
            PipeScript.PipeSpeed = _regularPipeSpeed;
            yield return new WaitForSeconds(5f);
            // Debug.Log("The PipeSpeed is now " + PipeScript.PipeSpeed);
        }
        while (_currentSpeedState == Speedstate._enumRegularPipeSpeed);
    }

    private IEnumerator BoostSpeedState()
    {
        Debug.Log("PipeSpeedLogic Boosted Speed State Activated");
        _currentSpeedState = Speedstate._enumBoostedPipeSpeed;

        // Increase the pipe speed exponentially by 1 percent every frame until a max speed
        do
        {
            PipeScript.PipeSpeed += PipeScript.PipeSpeed * 0.01f;
            // Debug.Log("The PipeSpeed is " + PipeScript.PipeSpeed);
            yield return null;
        }
        while (PipeScript.PipeSpeed > _regularPipeSpeed * _boostPipeSpeedMultiplier);

        Debug.Log("PipeSpeedLogic Entering max boost speed");

        // Sets the pipespeed to the actual value after acceleration
        PipeScript.PipeSpeed = _regularPipeSpeed * _boostPipeSpeedMultiplier;

    }

    private IEnumerator DecelSpeedState()
    {
        Debug.Log("PipeSpeedLogic Entering decel speed state.");
        _currentSpeedState = Speedstate._enumDecel;

        // Operation below will approximate an exponential decay using 4 time quadrants with 
        // and does it within the decel time duration
        float maxspeed = _regularPipeSpeed * _boostPipeSpeedMultiplier;
        float timecounter = 0f;

        // Use EaseFunc Easing functions with normalized time (0 to 1) timecounter/_decelDuration is normalizing it 
        while (timecounter < _decelDuration)
        {
            timecounter += Time.deltaTime;
            float t = Mathf.Clamp01(timecounter / _decelDuration);
            PipeScript.PipeSpeed = _easeFunction(maxspeed, _regularPipeSpeed, t);
            yield return null;
        }

        //At the end make sure to set the actual speed
        PipeScript.PipeSpeed = _regularPipeSpeed;
    }

    private IEnumerator StopSpeedState()
    {
        PipeScript.PipeSpeed = 0f;
        yield return null;
    }

    public void StateChangerRegularSpeed()
    {
        if (_currentStateCoroutine != null)
        {
            StopCoroutine(_currentStateCoroutine);
        }

        _currentStateCoroutine = StartCoroutine(RegularSpeedState());

    }

    public void StateChangerBoostSpeed()
    {
        if (_currentStateCoroutine != null)
        {
            StopCoroutine(_currentStateCoroutine);
        }

        _currentStateCoroutine = StartCoroutine(BoostSpeedState());
    }

    public void StateChangerDecel()
    {

        if (_currentStateCoroutine != null)
        {
            StopCoroutine(_currentStateCoroutine);
        }

        _currentStateCoroutine = StartCoroutine(DecelSpeedState());
    }
    
        public void StateChangerStop()
    {

        if (_currentStateCoroutine != null)
        {
            StopCoroutine(_currentStateCoroutine);
        }

        _currentStateCoroutine = StartCoroutine(StopSpeedState());
    }
    













}
