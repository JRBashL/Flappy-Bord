using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BordMainStateSO", menuName = "Scriptable Objects/BordMainState")]
public class BordStateMachineSO : ScriptableObject
{
    [SerializeField]
    private GameEvent BordBeginEvent, BordRegularSpeedEvent, BordBoostSpeedEvent, BordDeadEvent;

    [SerializeField]
    private BordMainFSM _bordMainState;

    public enum BordMainFSM
    {
        BeginState,
        RegularSpeedState,
        BoostSpeedState,
        DeadState
    }

    // Property
    public BordMainFSM BordMainState
    {
        get => _bordMainState;

        set
        {
            if (!Enum.IsDefined(typeof(BordMainFSM), value))
            {
                Debug.LogError("Setting wrong value for BordMainFSM. Setting to RegularSpeedState");
                _bordMainState = BordMainFSM.RegularSpeedState;
                BordRegularSpeedEvent.TriggerEvent();
            }

            else
            {

                _bordMainState = value;

                switch (value)
                {
                    case BordMainFSM.BeginState:

                        BordBeginEvent.TriggerEvent();
                        break;

                    case BordMainFSM.RegularSpeedState:

                        BordRegularSpeedEvent.TriggerEvent();
                        break;

                    case BordMainFSM.BoostSpeedState:

                        BordBoostSpeedEvent.TriggerEvent();
                        break;

                    case BordMainFSM.DeadState:

                        BordDeadEvent.TriggerEvent();
                        break;
                }
            }
        }
    }

    // Sets the default starting state
    void OnEnable()
    {
        _bordMainState = BordMainFSM.BeginState;
    }

    // Public methods for GameEventListeners if needed.
    public void StartBeginState()
    {
        BordMainState = BordMainFSM.BeginState;
    }

    public void StartRegularSpeedState()
    {
        BordMainState = BordMainFSM.RegularSpeedState;
    }

    public void StartBoostSpeedState()
    {
        BordMainState = BordMainFSM.BoostSpeedState;
    }

    public void StartDeadState()
    {
        BordMainState = BordMainFSM.DeadState;
    }
}
