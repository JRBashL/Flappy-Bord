using UnityEngine;
// Shorthanding the external script for the enum
using BordSOFSM = BordStateMachineSO.BordMainFSM;

// Script is handling the logic of collisions involved with the Bord
public class BordCollision : MonoBehaviour
{

    //Drag instance of BordFSMSO
    [SerializeField]
    private BordStateMachineSO _bordStateSO;

    private Rigidbody _bordRigidbody;
    private GameObject _otherGameObject;
    // Script dependent on reading the BordStateMachine enum
    private BordStateMachine _bordState;

    void Awake()
    {
        _bordRigidbody = GetComponent<Rigidbody>();
    }

    
    void OnTriggerEnter(Collider Col)
    {

        // Records the gameobject that the Bord collided with
        // Code is written in this way in case the collider is a child.       
        _otherGameObject = Col.gameObject.transform.root.gameObject;

        switch (_bordStateSO.BordMainState)
        {
            case BordSOFSM.BeginState:

                // Do nothing
                break;

            case BordSOFSM.RegularSpeedState:

                if (Col.gameObject.name == "Gap Collider")
                {
                    // Do nothing. The Gap Collider is responsible and will trigger the Score Event
                }

                else if (Col.CompareTag("Pipes"))
                {
                    Debug.Log("Bord Collided with the Pipes");

                    // Changes the state using the SOFSM
                    _bordStateSO.BordMainState = BordSOFSM.DeadState;

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

                else if (Col.CompareTag("Ground"))
                {
                    Debug.Log("Bord Collided with the Pipes");

                    // Changes the state using the SOFSM
                    _bordStateSO.BordMainState = BordSOFSM.DeadState;

                    // Makes the Bord a dynamic rigidbody and adds a random force vector.
                    _bordRigidbody.isKinematic = false;
                    _bordRigidbody.AddForce(
                        new Vector3(Random.Range(-2f, 2f),
                                    Random.Range(0f, 20f),
                                    Random.Range(-2f, 2f)
                        ),
                        ForceMode.Impulse
                    );
                }

                break;

            case BordSOFSM.BoostSpeedState:
                // Do Nothing. Maybe this can trigger some particles in the future.
                break;

            case BordSOFSM.DeadState:

                // Do nothing       
                break;
        }


        
    }






}


