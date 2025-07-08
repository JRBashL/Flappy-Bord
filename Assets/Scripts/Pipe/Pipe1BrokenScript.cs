using UnityEngine;

public class Pipe1BrokenScript : MonoBehaviour
{

    private Rigidbody _rb;

    void Awake()
    {
        // Get the Rigidbody component attached to this GameObject
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(0, 0, PipePrefabScript.PipeSpeed * Time.deltaTime);
    }
}
