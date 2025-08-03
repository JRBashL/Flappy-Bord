using UnityEngine;

public class Pipe1BrokenBaseScript : MonoBehaviour
{
    [SerializeField]
    private FloatVariable PipeSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, PipeSpeed.Value * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndofRoadCollider"))
        {
            Destroy(gameObject);          
        }
    }

}
