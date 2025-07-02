using UnityEngine;

public class SubPipeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _pipe1broken, _parentPipe;
    private Vector3 _currentPosVector3;


    void Awake()
    {
       // Get the parent pipe object
        _parentPipe = gameObject.transform.root.gameObject;
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
            _currentPosVector3 = _parentPipe.transform.position;
            Instantiate(_pipe1broken, _currentPosVector3, Quaternion.identity);
            _parentPipe.SetActive(false); // Disable the parent pipe to prevent further collisions
        }
    }

}
