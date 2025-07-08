using UnityEngine;

public class Pipe1BoxColliderScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _pipe1broken;
    private Vector3 _currentPosVector3;

    // Game Event to call when the pipe gets replaced with the broken version.
    [SerializeField]
    private GameEvent _pipeReplaceGameEvent;


    void Awake()
    {
     
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
            _pipeReplaceGameEvent.TriggerEvent();
        }
    }

}
