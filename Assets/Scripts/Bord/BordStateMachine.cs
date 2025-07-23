using System.Collections;
using UnityEngine;

public class BordStateMachine : MonoBehaviour
{
    public enum BordStateEnum
    {
        BeginState,
        RegularSpeedState,
        BoostSpeedState,
        DeadState

    };

    [SerializeField]
    private GameEvent BordBeginEvent, BordRegularSpeedEvent, BordBoostSpeedEvent, BordDeadEvent;

    public BordStateEnum CurrentBordState { get; private set; }
    private Coroutine _currentBordCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartBeginState();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public IEnumerator BeginState()
    {
        Debug.Log("BordStateMachine at BeginState");
        CurrentBordState = BordStateEnum.BeginState;
        BordBeginEvent.TriggerEvent();
        yield return null;
    }

    public IEnumerator RegularSpeedState()
    {
        Debug.Log("BordStateMachine at RegularSpeedState");
        CurrentBordState = BordStateEnum.RegularSpeedState;
        BordRegularSpeedEvent.TriggerEvent();
        yield return null;
    }

    public IEnumerator BoostSpeedState()
    {
        Debug.Log("BordStateMachine at BoostSpeedState");
        CurrentBordState = BordStateEnum.BoostSpeedState;
        BordBoostSpeedEvent.TriggerEvent();
        yield return null;
    }
    public IEnumerator DeadState()
    {
        Debug.Log("BordStateMachine at DeadState");
        CurrentBordState = BordStateEnum.DeadState;
        BordDeadEvent.TriggerEvent();
        yield return null;
    }

    // Public methods for GameEventListeners if needed.
    public void StartBeginState()
    {
        StopCurrentCoroutine();
        _currentBordCoroutine = StartCoroutine(BeginState());
    }

    public void StartRegularSpeedState()
    {
        StopCurrentCoroutine();
        _currentBordCoroutine = StartCoroutine(RegularSpeedState());
    }

    public void StartBoostSpeedState()
    {
        StopCurrentCoroutine();
        _currentBordCoroutine = StartCoroutine(BoostSpeedState());
    }

    public void StartDeadState()
    {
        StopCurrentCoroutine();
        _currentBordCoroutine = StartCoroutine(DeadState());
    }

    private void StopCurrentCoroutine()
    {
        if (_currentBordCoroutine != null)
        {
            StopCoroutine(_currentBordCoroutine);
            _currentBordCoroutine = null;
        }
    }
}
