using UnityEngine;

public class Pipe2BrokenPrefabScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1)
        {
            Destroy(gameObject);
        }
    }
}
