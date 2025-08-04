using UnityEngine;
using System.Collections;
// Shorthanding the external script for the enums
using PipeSpeedSOFSM = PipeSpeedStateSO.PipeSpeedFSM;

public class PipeSpeedLogic : MonoBehaviour
{

    // Declare the Scriptable Object that holds the data for the pipe speeds
    [SerializeField]
    [Tooltip("Place PipeSpeedScriptableObject here that contains the pipe speeds. Use it to configure regular and boosted speeds.")]
    private PipeScriptableObject _pipeSpeedScriptableObject;

    // Add the float variable
    [SerializeField]
    private FloatVariable PipeSpeed;

    // Declare pipe speeds and durations
    private float _regPipeSpeed, _pipeSpeedBoostMultiplier, _durationAccel, _durationBoost,
        _durationDecel, _increaseSpeedPerSecond;

    // Declare fallback speeds
    private float _pipeSpeedDefault = -15f;
    private float _pipeSpeedBoostMultiplierDefault = 3;
    private float _durationAccelDefault = 1f;
    private float _durationBoostDefault = 5f;
    private float _durationDecelDefault = 2f;
    private float _defaultIncreaseSpeedPerSecond = 0.05f;

    // Declare Easing Functions
    private EaseFunc.Ease _enumEaseOutQuint, _enumEaseLinear, _enumEaseInExpo;
    private EaseFunc.Function _functionEaseOutQuint, _functionLinear, _functionEaseInExpo;

    // Declare enum for state and couroutines
    [SerializeField] PipeSpeedStateSO _pipeSpeedStateSO;
    private Coroutine _stateCurrentCoroutine;

    // Declare GameEvents
    [SerializeField]
    private GameEvent _accelEvent, _speedBoostEvent, _decelEvent, _hasteFinished;

    // Declare boolean for watching state change in the update function through polling
    private bool _isStateChanged;
    private PipeSpeedSOFSM _previousState, _currentState;

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
            _durationAccel = _durationAccelDefault;
            _durationBoost = _durationBoostDefault;
            _durationDecel = _durationDecelDefault;
            _increaseSpeedPerSecond = _defaultIncreaseSpeedPerSecond;
        }
        else
        {
            _regPipeSpeed = _pipeSpeedScriptableObject.RegularPipeSpeed;
            _pipeSpeedBoostMultiplier = _pipeSpeedScriptableObject.BoostPipeSpeedMultiplier;
            _durationAccel = _pipeSpeedScriptableObject.AccelDuration;
            _durationBoost = _pipeSpeedScriptableObject.BoostDuration;
            _durationDecel = _pipeSpeedScriptableObject.DecelDuration;
            _increaseSpeedPerSecond = _pipeSpeedScriptableObject.IncreasingSpeedPerSec;
        }

        _enumEaseOutQuint = EaseFunc.Ease.EaseOutQuint;
        _functionEaseOutQuint = EaseFunc.GetEasingFunction(_enumEaseOutQuint);
        _enumEaseLinear = EaseFunc.Ease.Linear;
        _functionLinear = EaseFunc.GetEasingFunction(_enumEaseLinear);
        _enumEaseInExpo = EaseFunc.Ease.EaseInExpo;
        _functionEaseInExpo = EaseFunc.GetEasingFunction(_enumEaseInExpo);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // Set to zero at the beginning for polling
        _previousState = PipeSpeedSOFSM.ZeroPipeSpeed;
    }

    // Update is called once per frame
    void Update()
    {   
        //Poll the FSM
        _currentState = _pipeSpeedStateSO.PipeSpeedState;

        if (_currentState != _previousState)
        {
            
            switch (_pipeSpeedStateSO.PipeSpeedState)
            {
                case PipeSpeedSOFSM.RegularPipeSpeed:
                    StateChangerRegularSpeed();
                    break;
                case PipeSpeedSOFSM.AccelPipeSpeed:           
                    StateChangerAccelSpeed();
                    break;
                case PipeSpeedSOFSM.BoostedPipeSpeed:
                    // Coroutine is started by Accel Coroutine
                    //StateChangerBoostSpeed();
                    break;
                case PipeSpeedSOFSM.DecelPipeSpeed:
                    // Coroutine is started by Boosted Coroutine
                    //StateChangerDecel();
                    break;
                case PipeSpeedSOFSM.StopPipeSpeed:
                    StateChangerStop();
                    break;
                case PipeSpeedSOFSM.ZeroPipeSpeed:
                    StateChangerZero();
                    break;
            }
        }
        _previousState = _pipeSpeedStateSO.PipeSpeedState;
    }



    // Coroutine state on regular gameplay
    private IEnumerator StateRegularSpeed()
    {
        Debug.Log("PipeSpeedLogic Regular Speed State Activated.");

        PipeSpeed.Value = _regPipeSpeed;

        // Speed constantly increases
        do
        {
            _regPipeSpeed += _increaseSpeedPerSecond;
            PipeSpeed.Value = _regPipeSpeed;
            yield return new WaitForSeconds(1f);
            // Debug.Log("The PipeSpeed is now " + PipePrefabScript.PipeSpeed);
        }
        while (_pipeSpeedStateSO.PipeSpeedState == PipeSpeedSOFSM.RegularPipeSpeed);
    }

    private IEnumerator StateAccelSpeed()
    {
        Debug.Log("PipeSpeedLogic Accel Speed State Activated");

        // Increase the pipe speed exponentially by Easefunc until a max speed within the accel duration

        float timer = 0f;
        float startSpeed = PipeSpeed.Value;
        float maxSpeed = _regPipeSpeed * _pipeSpeedBoostMultiplier;

        do
        {
            float t = timer / _durationAccel;
            PipeSpeed.Value = _functionEaseInExpo(startSpeed, maxSpeed, t);
            timer += Time.deltaTime;
            yield return null;
        }
        while (PipeSpeed.Value > maxSpeed);

        StateChangerBoostSpeed();
    }

    private IEnumerator StateBoostSpeed()
    {
        Debug.Log("PipeSpeedLogic Entering max boost speed");

        float timer = 0f;
        // Sets the pipespeed to the actual value after acceleration
        PipeSpeed.Value = _regPipeSpeed * _pipeSpeedBoostMultiplier;

        while (timer < _durationBoost)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        StateChangerDecel();

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
            PipeSpeed.Value = _functionEaseOutQuint(maxspeed, _regPipeSpeed, t);
            timecounter += Time.deltaTime;
            yield return null;
        }

        _hasteFinished.TriggerEvent();
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
            PipeSpeed.Value = _functionLinear(_regPipeSpeed, 0f, t);
            timecounter += Time.deltaTime;
            yield return null;
        }

        PipeSpeed.Value = 0f;
    }

    private IEnumerator StateZeroSpeed()
    {
        Debug.Log("PipeSpeedLogic Entering Zero Speed");
        PipeSpeed.Value = 0f;
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

        //_regularSpeedEvent.TriggerEvent();

    }

    public void StateChangerAccelSpeed()
    {
        if (_stateCurrentCoroutine != null)
        {
            StopCoroutine(_stateCurrentCoroutine);
        }

        _stateCurrentCoroutine = StartCoroutine(StateAccelSpeed());

        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedSOFSM.AccelPipeSpeed;

        _accelEvent.TriggerEvent();
    }


    public void StateChangerBoostSpeed()
    {
        if (_stateCurrentCoroutine != null)
        {
            StopCoroutine(_stateCurrentCoroutine);
        }

        _stateCurrentCoroutine = StartCoroutine(StateBoostSpeed());

        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedSOFSM.BoostedPipeSpeed;

        _speedBoostEvent.TriggerEvent();
    }

    public void StateChangerDecel()
    {

        if (_stateCurrentCoroutine != null)
        {
            StopCoroutine(_stateCurrentCoroutine);
        }

        _stateCurrentCoroutine = StartCoroutine(StateDecelSpeed());

        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedSOFSM.DecelPipeSpeed;

        _decelEvent.TriggerEvent();
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
