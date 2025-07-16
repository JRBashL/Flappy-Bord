using Unity.VisualScripting;
using UnityEngine;

public class Pipe2BoxColliderScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _pipe2broken;
    private Vector3 _currentPosVector3;

    // To reference the prefab script in this prefab instance
    private GameObject _parentPipe;
    private PipePrefabScript _pipePrefabScript;

    // Game Event to call when the pipe gets replaced with the broken version.
    [SerializeField]
    private GameEvent _pipeReplaceGameEvent;


    void Awake()
    {
        // Get a reference of the Prefab script
        _parentPipe = gameObject.transform.root.gameObject;
        _pipePrefabScript = _parentPipe.GetComponent<PipePrefabScript>();
    
    }
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

        Debug.Log("SubPipeScript OnTriggerEnter activated with " + other.gameObject.name);


        if (other.gameObject.CompareTag("Bord") && PipePrefabScript._isboostedState)
        {
            // Game event for the future. Adding score etc
            _pipeReplaceGameEvent.TriggerEvent();
            _pipePrefabScript.Pipe2BreakHandler();
        }
    }

}
