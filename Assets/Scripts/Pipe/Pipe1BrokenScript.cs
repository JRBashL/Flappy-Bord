using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Pipe1BrokenScript : MonoBehaviour
{

    private Rigidbody _rb;
    private CapsuleCollider _ownCollider;
    private Vector3 _collisionLocation;
    private bool _ishit;
    private float _disappearTime;

    // Declare easing functions
    private EaseFunc.Ease _easeEnum;
    private EaseFunc.Function _easeFunc;

    //private MeshRenderer _gO;

    void Awake()
    {
        // Get the Rigidbody component attached to this GameObject
        _rb = gameObject.GetComponent<Rigidbody>();
        //_gO = gameObject.GetComponent<MeshRenderer>();
        _ownCollider = gameObject.GetComponent<CapsuleCollider>();

        // Assign enum as Linear
        _easeEnum = EaseFunc.Ease.Linear;
        // Assign Linear function to delegate
        _easeFunc = EaseFunc.GetEasingFunction(_easeEnum);

        _disappearTime = 3f;
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

           StartCoroutine(DeSpawner());


        }
    }

    private IEnumerator DeSpawner()
    {
        Renderer rend = GetComponent<Renderer>();
        Color myColor = rend.material.color;
        float alpha;

        //Use linear easing functions with normalized time
        float timecounter = 0f;

        while (timecounter < _disappearTime)
        {
            timecounter += Time.deltaTime;
            float t = Mathf.Clamp01(timecounter / _disappearTime);
            alpha = _easeFunc(1, 0, t);
            myColor.a = alpha;
            rend.material.color = myColor;
            yield return null;
        }

        Destroy(gameObject);
    } 

}

