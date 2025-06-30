using UnityEngine;

public class SubPipeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _pipe1BottomBase, _pipe1BottomBroken, _pipe1TopBroken, _pipe1TopBase, _parentPipe;
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

        if (other.CompareTag("Bord") && PipeScript._isboostedState)
        {
            _currentPosVector3 = _parentPipe.transform.position;
            Instantiate(gameObject, _currentPosVector3, Quaternion.identity);
        }
    }

}
