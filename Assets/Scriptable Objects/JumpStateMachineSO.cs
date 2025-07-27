using System;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpStateMachineSO", menuName = "Scriptable Objects/JumpStateMachine")]
public class JumpStateMachineSO : ScriptableObject
{

    // FSM for Bord Jumping
    public enum JumpFSM { NotJumping, RegularSpeedJumping, SpeedBoostJumping }
    [SerializeField] private JumpFSM _jumpState;

    // Property
    public JumpFSM JumpState
    {
        get
        {
            return _jumpState;
        }

        set
        {
            if (!Enum.IsDefined(typeof(JumpFSM), value))
            {
                Debug.LogError("Setting wrong value for JumpFSM. Setting to NotJumping");
                _jumpState = JumpFSM.NotJumping;
            }

            else
            {
                _jumpState = value;
            }
        }
    }

    // Set default starting value.
    void OnEnable()
    {
        _jumpState = JumpFSM.NotJumping;
    }

}
