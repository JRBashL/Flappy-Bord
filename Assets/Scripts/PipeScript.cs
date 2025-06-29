using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeScript : MonoBehaviour
{

    //PipeSpeed is going to be shared to all instances of the pipe in the game therefore it is public static
    public static float PipeSpeed;

    private GameObject _pipeGameObject;

    // Boolean listens to GameEvent
    [SerializeField]
    private static bool _isboostedState = false;
    private Rigidbody _rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pipeGameObject = gameObject.transform.root.gameObject;
        _rb = _pipeGameObject.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        // Clean reset of pipe from edge case of being hit in the isboosted state

    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, PipeSpeed * Time.deltaTime);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndofRoadCollider")) 
        {
            //Debug.Log("Pipe has collided with end of road.");
            // SetPipeSpeed(0f);
            // Deleted method when changing the PipeScript speed into a public static property
            _pipeGameObject.SetActive(false);
        }

        else if (other.CompareTag("Bord") && _isboostedState)
        {
            Debug.Log("EnumHitterandDisable Activated");
            Debug.Log("_isBoostedState = " + _isboostedState);
            StartCoroutine(HitterandDisabler());
        }
    }




    private IEnumerator HitterandDisabler()
    {
        _rb.isKinematic = false;
        _rb.AddForce(new Vector3(25f, 0, 25f), ForceMode.Impulse);
        _rb.AddTorque(new Vector3(0f, 0f, 10f), ForceMode.Impulse);
        yield return new WaitForSeconds(10f);
        _rb.linearVelocity = Vector3.zero;
        _rb.isKinematic = true;
        _pipeGameObject.SetActive(false);
    }

    public void BooleanChangerRegularSpeed()
    {
        Debug.Log("PipeScript BooleanChangerRegularSpeed Activated");
        _isboostedState = false;
        Debug.Log("_isBoostedState = " + _isboostedState);
    }

    public void BooleanChangerBoostedSpeed()
    {
        _isboostedState = true;
    }
}
