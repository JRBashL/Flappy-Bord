using UnityEngine;
using System.Collections;

public class GapColliderManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Place PipeSpeedScriptableObject here that contains the pipe speeds and durations. Use it to configure regular and boosted speeds.")]
    private PipeScriptableObject _pipeSpeedSO;


    [SerializeField]
    private float _speedBoostDuration, _decelDuration;

    // Declare fallback speeds
    private const float _defaultBoostDuration = 5f;
    private const float _defaultDecelDuration = 2f;

    [SerializeField]
    private GameEvent _speedBoostEvent, _regularSpeedEvent, _decelEvent;

    //bool to stop multiple coroutines from happening at the same time
    private static bool _isBoostEventHappening;


    // Initialize the fields
    void Awake()
    {
        if (_pipeSpeedSO == null)
        {
            Debug.Log("PipeSpeedScriptableObject not assigned in inspector for BoostCollider!");
            Debug.LogWarning("Setting fallback values.");
            _speedBoostDuration = _defaultBoostDuration;
            _decelDuration = _defaultDecelDuration;
        }
        else
        {
            _speedBoostDuration = _pipeSpeedSO.BoostDuration;
            _decelDuration = _pipeSpeedSO.DecelDuration;
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isBoostEventHappening = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private IEnumerator SpeedBoost()
    {
        Debug.Log("Boost Collider triggered SpeedBoost Game Event");
        _speedBoostEvent.TriggerEvent();
        yield return new WaitForSeconds(5f);
        Debug.Log("After " + _speedBoostDuration + " seconds, Boost Collider ends SpeedBoost Game Event");

        StartCoroutine(Decel());
    }

    private IEnumerator Decel()
    {
        Debug.Log("Boost Collider triggered Decel Game Event");
        _decelEvent.TriggerEvent();
        yield return new WaitForSeconds(_decelDuration);
        Debug.Log("After" + _decelDuration + "seconds, Boost Collider end Decel Game Event");

        _regularSpeedEvent.TriggerEvent();
        _isBoostEventHappening = false;

    }

}

