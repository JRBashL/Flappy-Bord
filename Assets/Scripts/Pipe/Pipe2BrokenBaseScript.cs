using UnityEngine;

public class Pipe2BrokenBaseScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, PipePrefabScript.PipeSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndofRoadCollider"))
        {
            Destroy(gameObject);          
        }
    }

}
