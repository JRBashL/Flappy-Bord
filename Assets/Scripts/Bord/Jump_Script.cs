using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump_Script : MonoBehaviour
{
    // Expose regular and speedboost velocities to Editor.
    [SerializeField] private float _regularJumpVelocity, _regularGravity, _speedBoostJumpVelocity, _speedBoostGravity;

    private bool _hasGameStarted;
    private float _regularGravityVelocity, _speedBoostGravityVelocity;

    // Get instance of the BordStatemachine
    private BordStateMachine _bordState;

    // Create Sub FSM for jumping
    private enum JumpState {NotJumping, RegularSpeedJumping, SpeedBoostJumping}
    private JumpState _jumpState;
    private Coroutine _jumpCoroutine;

    void Awake()
    {
        _bordState = GetComponent<BordStateMachine>();
        _jumpState = JumpState.NotJumping;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnJump()
    {
        // Check main state of Bord and handles sub FSM accordingly
        switch (_bordState.CurrentBordState)
        {
            case BordStateMachine.BordStateEnum.BeginState:

                _bordState.StartRegularSpeedState();
                _jumpState = JumpState.RegularSpeedJumping;
                StopJumpCoroutine();              
                _jumpCoroutine = StartCoroutine(Jump());
                break;

            case BordStateMachine.BordStateEnum.RegularSpeedState:
            
                StopJumpCoroutine();
                _jumpState = JumpState.RegularSpeedJumping;
                _jumpCoroutine = StartCoroutine(Jump());                              
                break;

            case BordStateMachine.BordStateEnum.BoostSpeedState:

                StopJumpCoroutine();
                _jumpState = JumpState.SpeedBoostJumping;
                _jumpCoroutine = StartCoroutine(Jump());
                break;

            case BordStateMachine.BordStateEnum.DeadState:
                break;
        }

    }


    private IEnumerator Jump()
    {
        // Resets falling speeds and starts to add it again every frame
        _regularGravityVelocity = 0;
        _speedBoostGravityVelocity = 0;
        
        // Logic to control jumping per frame. Coroutine checks each frame if in boosted state while coroutine is still running.
        while (_jumpState != JumpState.NotJumping)
        {
            switch (_bordState.CurrentBordState)
            {
                case BordStateMachine.BordStateEnum.RegularSpeedState:

                    if (_regularJumpVelocity > _regularGravityVelocity)
                    {
                        // Calculate movement based on decaying speed of +jump velocity and -gravity velocity
                        transform.Translate(new Vector3(0f, (_regularJumpVelocity - _regularGravityVelocity) * Time.deltaTime, 0f));
                        break;
                    }

                    // Once at the top of the jump arc is when jump velocity equals gravity velocity
                    else { goto endJumpWhileLoop; }
                    
                case BordStateMachine.BordStateEnum.BoostSpeedState:

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
            _regularGravityVelocity += _regularGravity * Time.deltaTime;
            _speedBoostGravityVelocity += _speedBoostGravity * Time.deltaTime;
            yield return null;

        }

        // Code jumps to here once jump velocity ends
        endJumpWhileLoop:
        // at the peak of the jump, the velocity is zero and the while loop ends. Reset values and end Coroutine.
        _regularGravityVelocity = 0;
        _speedBoostGravityVelocity = 0;
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

    // Update is called once per frame
    void Update()
    {
        switch (_bordState.CurrentBordState)
        {
            case BordStateMachine.BordStateEnum.BeginState:

                // Do not apply gravity at begin state
                break;

            case BordStateMachine.BordStateEnum.RegularSpeedState:

                if (_jumpState == JumpState.NotJumping)
                {
                    // Accumulate both gravity velocities for smooth transitions mid-fall between states.
                    _regularGravityVelocity += _regularGravity * Time.deltaTime;
                    _speedBoostGravityVelocity += _speedBoostGravity * Time.deltaTime;                    
                    transform.Translate(new Vector3(0, _regularGravityVelocity * -1, 0) * Time.deltaTime);
                }
                break;

            case BordStateMachine.BordStateEnum.BoostSpeedState:

                if (_jumpState == JumpState.NotJumping)
                {
                    // Accumulate both gravity velocities for smooth transitions mid-fall between states.
                    _regularGravityVelocity += _regularGravity * Time.deltaTime;
                    _speedBoostGravityVelocity += _speedBoostGravity * Time.deltaTime;
                    transform.Translate(new Vector3(0, _speedBoostGravityVelocity * -1 * Time.deltaTime, 0f));
                }
                break;

            case BordStateMachine.BordStateEnum.DeadState:

                break;
        }
    }

}