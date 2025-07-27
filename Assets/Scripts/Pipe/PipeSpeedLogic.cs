using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using UnityEngine.UIElements;
// Shorthanding the external script for the enums
using PipeSpeedSOFSM = PipeSpeedStateSO.PipeSpeedFSM;

public class PipeSpeedLogic : MonoBehaviour
{

    // Declare the Scriptable Object that holds the data for the pipe speeds

    [SerializeField]
    [Tooltip("Place PipeSpeedScriptableObject here that contains the pipe speeds. Use it to configure regular and boosted speeds.")]
    private PipeScriptableObject _pipeSpeedScriptableObject;

    // Declare pipe speeds and durations
    [SerializeField]
    private float _regPipeSpeed, _pipeSpeedBoostMultiplier, _durationBoost, _durationDecel, _currentPipeSpeed, _increaseSpeedPerSecond;

    // Declare fallback speeds
    private const float _pipeSpeedDefault = -15f;
    private const float _pipeSpeedBoostMultiplierDefault = 3;
    private const float _duartionBoostDefault = 5f;
    private const float _durationDecelDefault = 2f;
    private const float _defaultIncreaseSpeedPerSecond = 0.05f;

    // Declare Easing Functions
    private EaseFunc.Ease _enumEaseOutQuint, _enumEaseLinear;
    private EaseFunc.Function _functionEaseOutQuint, _functionLinear;

    // Declare enum for state and couroutines
    [SerializeField] PipeSpeedStateSO _pipeSpeedStateSO;
    private Coroutine _stateCurrentCoroutine;

    void Awake()
    {

        // If statement to initialze the pipe speed fields. Has a fallback in case the scriptable object to be assigned in the inspector
        // isn't placed.
        if (_pipeSpeedScriptableObject == null)
        {
            Debug.LogWarning("PipeSpeedScriptableObject not assigned in inspector for PipeSpeedLogic!");
            Debug.LogWarning("Setting fallback values.");
            _regPipeSpeed = _pipeSpeedDefault;
            _pipeSpeedBoostMultiplier = _pipeSpeedBoostMultiplierDefault;
            _durationBoost = _duartionBoostDefault;
            _durationDecel = _durationDecelDefault;
            _increaseSpeedPerSecond = _defaultIncreaseSpeedPerSecond;
        }
        else
        {
            _regPipeSpeed = _pipeSpeedScriptableObject.RegularPipeSpeed;
            _pipeSpeedBoostMultiplier = _pipeSpeedScriptableObject.BoostPipeSpeedMultiplier;
            _durationBoost = _pipeSpeedScriptableObject.BoostDuration;
            _durationDecel = _pipeSpeedScriptableObject.DecelDuration;
            _increaseSpeedPerSecond = _pipeSpeedScriptableObject.IncreasingSpeedPerSec;
        }

        _enumEaseOutQuint = EaseFunc.Ease.EaseOutQuint;
        _functionEaseOutQuint = EaseFunc.GetEasingFunction(_enumEaseOutQuint);
        _enumEaseLinear = EaseFunc.Ease.Linear;
        _functionLinear = EaseFunc.GetEasingFunction(_enumEaseLinear);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    // Coroutine state on regular gameplay
    private IEnumerator StateRegularSpeed()
    {
        Debug.Log("PipeSpeedLogic Regular Speed State Activated.");

        PipePrefabScript.PipeSpeed = _regPipeSpeed;

        // Speed constantly increases
        do
        {
            _regPipeSpeed += _increaseSpeedPerSecond;
            PipePrefabScript.PipeSpeed = _regPipeSpeed;
            yield return new WaitForSeconds(5f);
            // Debug.Log("The PipeSpeed is now " + PipePrefabScript.PipeSpeed);
        }
        while (_pipeSpeedStateSO.PipeSpeedState == PipeSpeedSOFSM.RegularPipeSpeed);
    }

    private IEnumerator StateBoostSpeed()
    {
        Debug.Log("PipeSpeedLogic Boosted Speed State Activated");

        // Increase the pipe speed exponentially by 1 percent every frame until a max speed
        do
        {
            PipePrefabScript.PipeSpeed += PipePrefabScript.PipeSpeed * 0.01f;
            // Debug.Log("The PipeSpeed is " + PipePrefabScript.PipeSpeed);
            yield return null;
        }
        while (PipePrefabScript.PipeSpeed > _regPipeSpeed * _pipeSpeedBoostMultiplier);

        Debug.Log("PipeSpeedLogic Entering max boost speed");

        // Sets the pipespeed to the actual value after acceleration
        PipePrefabScript.PipeSpeed = _regPipeSpeed * _pipeSpeedBoostMultiplier;

    }

    private IEnumerator StateDecelSpeed()
    {
        Debug.Log("PipeSpeedLogic Entering decel speed state.");

        // Operation below will approximate an exponential decay using 4 time quadrants with 
        // and does it within the decel time duration
        float maxspeed = _regPipeSpeed * _pipeSpeedBoostMultiplier;
        float timecounter = 0f;

        // Use EaseFunc Easing functions with normalized time (0 to 1) timecounter/_durationDecel is normalizing it 
        while (timecounter < _durationDecel)
        {
            float t = Mathf.Clamp01(timecounter / _durationDecel);
            PipePrefabScript.PipeSpeed = _functionEaseOutQuint(maxspeed, _regPipeSpeed, t);
            timecounter += Time.deltaTime;
            yield return null;
        }

        //At the end make sure to set the actual speed
        PipePrefabScript.PipeSpeed = _regPipeSpeed;
    }

    private IEnumerator StateStopSpeed()
    {
        Debug.Log("PipeSpeedLogic Entering stop speed state");
        float timecounter = 0f;
        float duration = 0.5f;

        while (timecounter < duration)
        {
            // Goes from current speed to 0 speed in half a second. Normalized for Easefunc
            float t = Mathf.Clamp01(timecounter / duration);
            PipePrefabScript.PipeSpeed = _functionLinear(_regPipeSpeed, 0f, t);
            timecounter += Time.deltaTime;
            yield return null;
        }

        PipePrefabScript.PipeSpeed = 0f;
    }

    private IEnumerator StateZeroSpeed()
    {
        Debug.Log("PipeSpeedLogic Entering Zero Speed");
        PipePrefabScript.PipeSpeed = 0f;
        yield return null;
    }

    public void StateChangerRegularSpeed()
    {
        if (_stateCurrentCoroutine != null)
        {
            StopCoroutine(_stateCurrentCoroutine);
        }

        _stateCurrentCoroutine = StartCoroutine(StateRegularSpeed());

        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedSOFSM.RegularPipeSpeed;

    }

    public void StateChangerBoostSpeed()
    {
        if (_stateCurrentCoroutine != null)
        {
            StopCoroutine(_stateCurrentCoroutine);
        }

        _stateCurrentCoroutine = StartCoroutine(StateBoostSpeed());

        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedSOFSM.BoostedPipeSpeed;
    }

    public void StateChangerDecel()
    {

        if (_stateCurrentCoroutine != null)
        {
            StopCoroutine(_stateCurrentCoroutine);
        }

        _stateCurrentCoroutine = StartCoroutine(StateDecelSpeed());

        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedSOFSM.DecelPipeSpeed;
    }

    public void StateChangerStop()
    {

        if (_stateCurrentCoroutine != null)
        {
            StopCoroutine(_stateCurrentCoroutine);
        }

        _stateCurrentCoroutine = StartCoroutine(StateStopSpeed());

        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedSOFSM.StopPipeSpeed;
    }

    public void StateChangerZero()
    {
        if (_stateCurrentCoroutine != null)
        {
            StopCoroutine(_stateCurrentCoroutine);
        }

        _stateCurrentCoroutine = StartCoroutine(StateZeroSpeed());

        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedSOFSM.ZeroPipeSpeed;
    }














}
