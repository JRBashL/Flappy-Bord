using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Pipe1BrokenScript : MonoBehaviour
{

    private Rigidbody _rb;
    private CapsuleCollider _ownCollider;
    private Vector3 _collisionLocation;
    private bool _ishit;

    [SerializeField]
    private float _disappearTime, _waitForDisappearTime;

    // Declare easing functions
    private EaseFunc.Ease _easeEnum;
    private EaseFunc.Function _easeFunc;

    // Declare material for alpha control and fade out.
    private Material _material;
    private Renderer _rend;

    void Awake()
    {
        // Get the components attached to this GameObject
        _rb = gameObject.GetComponent<Rigidbody>();
        _rend = gameObject.GetComponent<Renderer>();
        _material = _rend.material;
        _ownCollider = gameObject.GetComponent<CapsuleCollider>();

        // Assign enum as Linear
        _easeEnum = EaseFunc.Ease.Linear;
        // Assign Linear function to delegate
        _easeFunc = EaseFunc.GetEasingFunction(_easeEnum);

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndofRoadCollider"))
        {
            Destroy(gameObject);          
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

           StartCoroutine(DeSpawner());


        }
    }

    private IEnumerator DeSpawner()
    {
        yield return new WaitForSeconds(_waitForDisappearTime);

        float alpha;

        //Use linear easing functions with normalized time
        float timecounter = 0f;

        while (timecounter < _disappearTime)
        {
            timecounter += Time.deltaTime;
            float t = Mathf.Clamp01(timecounter / _disappearTime);
            alpha = _easeFunc(1, 0, t);
       
            _material.SetFloat("_alpha", alpha);
            yield return null;
        }

        Destroy(gameObject);
    } 

}

