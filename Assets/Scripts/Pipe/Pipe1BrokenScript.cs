using UnityEngine;

public class Pipe1BrokenScript : MonoBehaviour
{

    private Rigidbody _rb;
    private CapsuleCollider _ownCollider;
    private Vector3 _collisionLocation;
    private bool _ishit;

    void Awake()
    {
        // Get the Rigidbody component attached to this GameObject
        _rb = gameObject.GetComponent<Rigidbody>();
        _ownCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_ishit)
        {
            transform.Translate(0, 0, PipePrefabScript.PipeSpeed * Time.deltaTime);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Pipe1Brokenscript detected collision with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Bord") && !_ishit)
        {
            _ishit = true;
            _rb.useGravity = true;

            Debug.Log("Pipe1Brokenscript detected collision with Bord");

            _collisionLocation = collision.transform.position;
            _rb.AddForceAtPosition(
                new Vector3(Random.Range(-75, 75), Random.Range(-75, 75), Random.Range(50, 100)),
                _collisionLocation,
                ForceMode.Impulse);

            _ownCollider.enabled = false;

            
        }
    }



}

