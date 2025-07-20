using Unity.VisualScripting;
using UnityEngine;

public class PipeBoxColliderScript : MonoBehaviour
{
    [SerializeField]

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
  
    void OnTriggerEnter(Collider other)
    {

        Debug.Log("SubPipeScript OnTriggerEnter activated with " + other.gameObject.name);


        if (other.gameObject.CompareTag("Bord") && PipePrefabScript._isboostedState)
        {
            // Game event for the future. Adding score etc
            _pipeReplaceGameEvent.TriggerEvent();
            _pipePrefabScript.PipeBreakHandler();
        }
    }

}
