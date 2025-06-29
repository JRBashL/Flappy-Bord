using UnityEngine;
using UnityEngine.InputSystem;

public class Jump_Script : MonoBehaviour

{

    // This is the upwards velocity that is applied by the jump. It's an constant velocity and is like if a constant instantaneous
    // force is applied to the object.
    [SerializeField] private float _jumpVelocity; // 20f is good
    [SerializeField] private float _bordGravity; // 30f is good
    private bool _hasGameStarted;
    private bool _isJumping;
    private float _fakeGravityVelocity;

    //this is declaring the variable "pcontrols" as the Unity's input system asset
    // I spent a while with this not working because I didn't know that  I had to auto-generate a class in C# for it so I can call it
    // in this script
    private Player_Actions_for_Bord pcontrols;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hasGameStarted = false;
        _isJumping = false;
    }

    private void OnJump()
    {
        // Starts falling only after the player has pressed click
        _hasGameStarted = true;
        _isJumping = true;
        // Resets falling speed on key press
        _fakeGravityVelocity = 0;
    }

    //Method applied the jump but it seems to only change the velocity in one frame. Since it's a kinematic object, I can't store velocity
    private void FakeJump(ref float JumpVelocity, ref bool IsJumping)
    {
        // This is the regular gravity that is increasing every frame
        _fakeGravityVelocity += _bordGravity * Time.deltaTime;

        // If statement to create an arc to the jump, essentially a way to calculate the jump force and the gravity force
        // but instead since it's a kinematic body, all are changed to translation of coordinates only
        if (JumpVelocity > _fakeGravityVelocity)   // This means that there is still an upward force here, and is still moving up
        {
            transform.Translate(new Vector3(0f, (JumpVelocity - _fakeGravityVelocity) * Time.deltaTime, 0f));
        }
        else // at the peak of the jump, the velocity is zero
        {
            IsJumping = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (_hasGameStarted == true & _isJumping == true)
        {
            //Apply the method fakeJump which is upwards force minus gravity force
            FakeJump(ref _jumpVelocity, ref _isJumping); 
        }
        else if (_hasGameStarted == true)
        {
            // Apply regular gravity
            _fakeGravityVelocity += _bordGravity * Time.deltaTime;
            transform.Translate(new Vector3(0, _fakeGravityVelocity * -1, 0) * Time.deltaTime);
        }
        else
        {
            return;
        }
        
        

    }

}