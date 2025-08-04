using UnityEngine;
using TMPro;

public class TextOutput : MonoBehaviour
{
    // ScriptableObjects FSMs
    [SerializeField] private BoostColliderStateSO _boostColliderStateSO;
    [SerializeField] private BordStateMachineSO _bordStateMachineSO;
    [SerializeField] private JumpStateMachineSO _jumpStateMachineSO;
    [SerializeField] private LaneStateMachineSO _laneStateMachineSO;
    [SerializeField] private PipeSpawnStateMachineSO _pipeSpawnStatemachineSO;
    [SerializeField] private PipeSpeedStateSO _pipeSpeedStateSO;

    private TMP_Text _tmText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tmText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _tmText.text =
            $"Boost Collider State: {_boostColliderStateSO.BoostColliderState}\n" +
            $"Bord State: {_bordStateMachineSO.BordMainState}\n" +
            $"Jump State: {_jumpStateMachineSO.JumpState}\n" +
            $"Lane: {_laneStateMachineSO.CurrentLaneState}\n" +
            $"Lane Change State: {_laneStateMachineSO.LaneChangeState}\n" +
            $"Pipe Spawn State: {_pipeSpawnStatemachineSO.PipeSpawnState}\n" +
            $"Pipe Speed State: {_pipeSpeedStateSO.PipeSpeedState}";

    }   
}
