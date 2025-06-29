using Unity.VisualScripting;
using UnityEngine;

// Script is handling the logic of collisions involved with the Bord
public class BordCollision : MonoBehaviour
{
    private Rigidbody _bordRigidbody;
    private GameObject _otherGameObject;
    // Script dependent on reading the BordStateMachine enum
    private BordStateMachine _bordState;

    void Awake()
    {
        _bordRigidbody = GetComponent<Rigidbody>();
        _bordState = GetComponent<BordStateMachine>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider Col)
    {

        // Records the gameobject that the Bord collided with
        // Code is written in this way in case the collider is a child.       
        _otherGameObject = Col.gameObject.transform.root.gameObject;

        switch (_bordState.CurrentBordState)
        {
            case BordStateMachine.BordStateEnum.BeginState:

                // Do nothing
                break;

            case BordStateMachine.BordStateEnum.RegularSpeedState:

                if (Col.gameObject.name == "Gap Collider")
                {
                    // Do nothing. The Gap Collider is responsible and will trigger the Score Event
                }

                else if (Col.CompareTag("Pipes"))
                {
                    Debug.Log("Bord Collided with the Pipes");

                    // Changes the coroutine state of the BordStateMachine
                    _bordState.StartDeadState();

                    // Makes the Bord a dynamic rigidbody and adds a random force vector.
                    _bordRigidbody.isKinematic = false;
                    _bordRigidbody.AddForce(
                        new Vector3(Random.Range(-10f, 10f),
                                    Random.Range(-10f, 10f),
                                    Random.Range(-10f, 10f)
                        ),
                        ForceMode.Impulse
                    );
                }

                break;

            case BordStateMachine.BordStateEnum.BoostSpeedState:
                // Do Nothing. Maybe this can trigger some particles in the future.

                break;
            case BordStateMachine.BordStateEnum.DeadState:
                // Do nothing       
                break;
        }


        
    }






}


