using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PipePrefabScript : MonoBehaviour
{

    //PipeSpeed is going to be shared to all instances of the pipe in the game therefore it is public static
    public static float PipeSpeed;

    private GameObject _pipeGameObject;

    // Serialize the Pipe 1 broken, Pipe 2 broken, Pipe 3 broken GameObject for the substitution
    [SerializeField]
    private GameObject _pipe1broken, _pipe2broken, _pipe3broken;

    // Boolean listens to GameEvent
    [SerializeField]
    public static bool _isboostedState = false;
    private Rigidbody _rb;

    // boolean for pipe substitution. To stop multiple instantiates
    private bool _ispipeBroken;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pipeGameObject = gameObject.transform.root.gameObject;
        _rb = _pipeGameObject.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        // Clean reset of pipe from boosted mode substitution
        _ispipeBroken = false;

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
/*
        else if (other.CompareTag("Bord") && _isboostedState)
        {
            Debug.Log("EnumHitterandDisable Activated");
            Debug.Log("_isBoostedState = " + _isboostedState);
            StartCoroutine(HitterandDisabler());
        }

        else if (other.CompareTag("Bord") && _isboostedState)
        {
            Debug.Log("PipeScript HitterandDisabler Activated");
            Debug.Log("_isBoostedState = " + _isboostedState);
            StartCoroutine(HitterandDisabler());
        }
*/
    }

    /// <summary>
    /// This function handles the substitution of the the pipe into the broken version. The function gets called from
    /// the box collider on the children of the pipe prefab. It also has the boolean to make sure there is only one 
    /// instantiate
    /// </summary>
    public void Pipe1BreakHandler()
    {
        if (_ispipeBroken)
        {
            return;
        }

        else
        {

            _ispipeBroken = true;

            Vector3 _currentPosVector3;
            _currentPosVector3 = transform.position;
        
            Instantiate(_pipe1broken, _currentPosVector3, Quaternion.identity);
            _pipeGameObject.SetActive(false); // Disable the parent pipe to prevent further collisions 
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
        Debug.Log("PipeScript BooleanChangerRegularSpeed() Activated");
        _isboostedState = false;
        Debug.Log("PipePrefabScript _isBoostedState = " + _isboostedState);
    }

    public void BooleanChangerBoostedSpeed()
    {
        _isboostedState = true;
        Debug.Log("PipeScript BooleanChangerBoostedSpeed() Activated");
        Debug.Log("PipePrefabScript _isBoostedState = " + _isboostedState);
    }
}
