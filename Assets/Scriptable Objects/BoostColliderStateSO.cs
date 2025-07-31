using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BoostColliderStateSO", menuName = "Scriptable Objects/BoostColliderStateSO")]
public class BoostColliderStateSO : ScriptableObject
{
    [SerializeField]
    private BoostColliderFSM _boostColliderState;

    public enum BoostColliderFSM
    {
        AliveState,
        DeadState
    }

    // Property
    public BoostColliderFSM BoostColliderState
    {
        get => _boostColliderState;

        set
        {
            if (!Enum.IsDefined(typeof(BoostColliderFSM), value))
            {
                Debug.LogError("Setting wrong value for BoostColliderFSM. Setting to AliveState");
                _boostColliderState = BoostColliderFSM.AliveState;
            }

            else
            {
                _boostColliderState = value;
            }
        }
    }


    // Set default starting value
    void OnEnable()
    {
        BoostColliderState = BoostColliderFSM.AliveState;
    }

    // Methods for Events
    public void SetAliveState()
    {
        BoostColliderState = BoostColliderFSM.AliveState;
    }

    public void SetDeadState()
    {
        BoostColliderState = BoostColliderFSM.DeadState;
    }

}



