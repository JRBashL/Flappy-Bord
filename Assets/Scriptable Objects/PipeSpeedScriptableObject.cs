using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PipeScriptableObject", menuName = "Scriptable Objects/PipeScriptableObject")]
public class PipeScriptableObject : ScriptableObject
{

    [SerializeField]
    private float _regularPipeSpeed;
    public float RegularPipeSpeed
    {
        get { return _regularPipeSpeed; }
        private set { _regularPipeSpeed = value; }
    }

    [SerializeField]
    private float _boostPipeSpeedMultiplier;
    public float BoostPipeSpeedMultiplier
    {
        get { return _boostPipeSpeedMultiplier; }
        private set { _boostPipeSpeedMultiplier = value; }
    }

    [SerializeField]
    private float _boostDuration;
    public float BoostDuration
    {
        get { return _boostDuration; }
        private set { _boostDuration = value; }
    }

    [SerializeField]
    private float _decelDuration;
    public float DecelDuration
    {
        get { return _decelDuration; }
        private set { _decelDuration = value; }
    }

    [SerializeField]
    private float _increasingSpeedPerSec;
    public float IncreasingSpeedPerSec
    {
        get { return _increasingSpeedPerSec; }
        private set { _increasingSpeedPerSec = value; }
    }




}
