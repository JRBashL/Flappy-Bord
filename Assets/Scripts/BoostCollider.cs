using System.Collections;
using UnityEngine;

public class BoostCollider : MonoBehaviour
{
    [SerializeField]
    public GameEvent _speedBooostTrigger;


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
        if (other.gameObject.CompareTag("Bord"))
        {
            // Speedboost triggers Decel then triggers Regular speed event.
            _speedBooostTrigger.TriggerEvent(); 

            Debug.Log("Boost Collider triggered SpeedBoost Game Event");
        }

       
    }

}
