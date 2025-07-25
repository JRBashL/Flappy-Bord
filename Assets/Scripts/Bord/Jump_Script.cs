using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
// Shorthanding the external script for the enum
using BordStateSOFSM = BordStateMachineSO.BordMainFSM;

public class Jump_Script : MonoBehaviour
{
    // Expose regular and speedboost velocities to Editor.
    [SerializeField] private float _regularJumpVelocity, _regularGravity, _speedBoostJumpVelocity, _speedBoostGravity;

    private float _regularGravityVelocity, _speedBoostGravityVelocity;

    // Get instance of the BordStateMachineSO
    [SerializeField] private BordStateMachineSO _bordStateSO;

    // Create Sub FSM for jumping
    private enum JumpState { NotJumping, RegularSpeedJumping, SpeedBoostJumping }
    private JumpState _jumpState;
    private Coroutine _jumpCoroutine;

    void Awake()
    {
        if (_bordStateSO == null)
        {
            Debug.LogWarning("Jump_Script needs BordStateMachineSO scriptable object to drag in!");
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
        }

        _jumpState = JumpState.NotJumping;

    }

    // Update is called once per frame. In update, gravity is applied depending on the BordStateSOFSM
    void Update()
    {
        switch (_bordStateSO.BordMainState)
        {
            case BordStateSOFSM.BeginState:

                // Do not apply gravity at begin state
                break;

            case BordStateSOFSM.RegularSpeedState:

                if (_jumpState == JumpState.NotJumping)
                {
                    // Accumulate both gravity velocities for smooth transitions mid-fall between states.
                    AddGravityVelocities();
                    transform.Translate(new Vector3(0, _regularGravityVelocity * -1, 0) * Time.deltaTime);
                }
                break;

            case BordStateSOFSM.BoostSpeedState:

                if (_jumpState == JumpState.NotJumping)
                {
                    // Accumulate both gravity velocities for smooth transitions mid-fall between states.
                    AddGravityVelocities();
                    transform.Translate(new Vector3(0, _speedBoostGravityVelocity * -1 * Time.deltaTime, 0f));
                }
                break;

            case BordStateSOFSM.DeadState:

                break;
        }
    }


    private void OnJump()
    {
        // Check main state of Bord and handles sub FSM accordingly
        switch (_bordStateSO.BordMainState)
        {
            case BordStateSOFSM.BeginState:

                //Set the enum of the SO FSM
                _bordStateSO.BordMainState = BordStateSOFSM.RegularSpeedState;
                _jumpState = JumpState.RegularSpeedJumping;
                StopJumpCoroutine();
                _jumpCoroutine = StartCoroutine(Jump());
                break;

            case BordStateSOFSM.RegularSpeedState:

                StopJumpCoroutine();
                _jumpState = JumpState.RegularSpeedJumping;
                _jumpCoroutine = StartCoroutine(Jump());
                break;

            case BordStateSOFSM.BoostSpeedState:

                StopJumpCoroutine();
                _jumpState = JumpState.SpeedBoostJumping;
                _jumpCoroutine = StartCoroutine(Jump());
                break;

            case BordStateSOFSM.DeadState:
                break;
        }

    }

    private IEnumerator Jump()
    {
        // Resets falling speeds and starts to add it again every frame
        ResetGravityVelocities();

        // Logic to control jumping per frame. Coroutine checks each frame if in boosted state while coroutine is still running.
        while (_jumpState != JumpState.NotJumping)
        {
            switch (_bordStateSO.BordMainState)
            {
                case BordStateSOFSM.RegularSpeedState:

                    if (_regularJumpVelocity > _regularGravityVelocity)
                    {
                        // Calculate movement based on decaying speed of +jump velocity and -gravity velocity
                        transform.Translate(new Vector3(0f, (_regularJumpVelocity - _regularGravityVelocity) * Time.deltaTime, 0f));
                        break;
                    }

                    // Once at the top of the jump arc is when jump velocity equals gravity velocity
                    else { goto endJumpWhileLoop; }

                case BordStateSOFSM.BoostSpeedState:

                    if (_speedBoostJumpVelocity > _speedBoostGravityVelocity)
                    {
                        // Calculate movement based on decaying speed of +jump velocity and -gravity velocity
                        transform.Translate(new Vector3(0f, (_speedBoostJumpVelocity - _speedBoostGravityVelocity) * Time.deltaTime, 0f));
                        break;
                    }

                    // Once at the top of the jump arc is when jump velocity equals gravity velocity
                    else { goto endJumpWhileLoop; }
            }

            // Add gravity velocity for next frame calc. Accumulate both gravity velocities for smooth transitions mid-jump.
            AddGravityVelocities();
            yield return null;

        }

        // Code jumps to here once jump velocity ends
        endJumpWhileLoop:
        // at the peak of the jump, the velocity is zero and the while loop ends. Reset values and end Coroutine.
        ResetGravityVelocities();
        _jumpCoroutine = null;
        _jumpState = JumpState.NotJumping;
    }

    private void StopJumpCoroutine()
    {
        if (_jumpCoroutine != null)
        {
            StopCoroutine(_jumpCoroutine);
        }
    }

    private void ResetGravityVelocities()
    {
        _regularGravityVelocity = 0;
        _speedBoostGravityVelocity = 0;
    }

    private void AddGravityVelocities()
    {
        _regularGravityVelocity += _regularGravity * Time.deltaTime;
        _speedBoostGravityVelocity += _speedBoostGravity * Time.deltaTime;
    }
 

}