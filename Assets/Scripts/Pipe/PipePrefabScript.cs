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

        Debug.Log(_pipeGameObject.name);
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
            _pipeGameObject.SetActive(false);          
        }

    }

    /// <summary>
    /// This function handles the substitution of the the pipe into the broken version. The function gets called from
    /// the box collider on the children of the pipe prefab. It also has the boolean to make sure there is only one 
    /// instantiate
    /// </summary>
    public void PipeBreakHandler()
    {
        if (_ispipeBroken)
        {
            return;
        }

        else
        {

            _ispipeBroken = true;

            Vector3 currentPosVector3;
            currentPosVector3 = transform.position;

            switch (_pipeGameObject.name)
            {
                case "Pipe 1(Clone)":
                    BrokenPipeSpawner(_pipe1broken, currentPosVector3);
                    break;
                case "Pipe 2(Clone)":
                    BrokenPipeSpawner(_pipe2broken, currentPosVector3);
                    break;
                case "Pipe 3(Clone)":
                    BrokenPipeSpawner(_pipe3broken, currentPosVector3);
                    break;
                default:
                    Debug.Log("PipeBreakHandler couldn't find a broken pipe to spawn!");
                    break;
            }
            _pipeGameObject.SetActive(false); // Disable the parent pipe to prevent further collisions 
        }
    }

    private void BrokenPipeSpawner(GameObject brokenpipe, Vector3 currentPosVector3)
    {
        Instantiate(brokenpipe, currentPosVector3, Quaternion.identity);
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
