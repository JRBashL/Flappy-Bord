using System.Collections;
using UnityEngine;
// Shorthand enum
using BoostColliderSOFSM = BoostColliderStateSO.BoostColliderFSM;

public class BoostCollider : MonoBehaviour
{
    [SerializeField]
    public GameEvent _speedBooostTrigger;

    [SerializeField]
    private BoostColliderStateSO _boostColliderState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        switch (_boostColliderState.BoostColliderState)
        {
            case BoostColliderSOFSM.AliveState:

                if (other.gameObject.CompareTag("Bord"))
                {
                    // Speedboost triggers Decel then triggers Regular speed event.
                    _speedBooostTrigger.TriggerEvent();

                    Debug.Log("Boost Collider triggered SpeedBoost Game Event");
                }

                break;

            case BoostColliderSOFSM.DeadState:

                // Do nothing

                break;
        }

    }

}
