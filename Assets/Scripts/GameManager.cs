using UnityEngine;
using BoostColliderStateSOFSM = BoostColliderStateSO.BoostColliderFSM;
using BordStateMachineSOFSM = BordStateMachineSO.BordMainFSM;
using JumpStateMachineSOFSM = JumpStateMachineSO.JumpFSM;
using LaneStateMachineSOCLFSM = LaneStateMachineSO.CurrentLaneFSM;
using LaneStateMachineSOLCFSM = LaneStateMachineSO.LaneChangeFSM;
using PipeSpawnStateMachineSOFSM = PipeSpawnStateMachineSO.PipeSpawnFSM;
using PipeSpeedStateSOFSM = PipeSpeedStateSO.PipeSpeedFSM;
using GameStateSOFSM = GameStateMachineSO.GameFSM;

using System.Collections;


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameStateMachineSO _gameState;
    public Coroutine GameCoroutine;

    // ScriptableObjects FSMs
    [SerializeField] private BoostColliderStateSO _boostColliderStateSO;
    [SerializeField] private BordStateMachineSO _bordStateMachineSO;
    [SerializeField] private JumpStateMachineSO _jumpStateMachineSO;
    [SerializeField] private LaneStateMachineSO _laneStateMachineSO;
    [SerializeField] private PipeSpawnStateMachineSO _pipeSpawnStatemachineSO;
    [SerializeField] private PipeSpeedStateSO _pipeSpeedStateSO;


    // GameEvents
    [SerializeField] private GameEvent _accelEvent, _decelEvent, _regularSpeedEvent, _speedBoostEvent;


    void Start()
    {
        GameCoroutine = StartCoroutine(StartGameCoroutine());
    }

    // Public methods to be called by GameEventListeners via UnityEvent in inspector

    /// <summary>
    /// Coroutine is for setting up the states of the Boost Collider, Bord, Pipe Spawning, Pipe Speed, Jumping, 
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartGameCoroutine()
    {
        // Set states when starting the game

        _gameState.GameState = GameStateSOFSM.GameStart;

        _boostColliderStateSO.BoostColliderState = BoostColliderStateSOFSM.AliveState;

        _bordStateMachineSO.BordMainState = BordStateMachineSOFSM.BeginState;

        _jumpStateMachineSO.JumpState = JumpStateMachineSOFSM.NotJumping;

        _laneStateMachineSO.CurrentLaneState = LaneStateMachineSOCLFSM.Center;
        _laneStateMachineSO.LaneChangeState = LaneStateMachineSOLCFSM.OnLane;

        _pipeSpawnStatemachineSO.PipeSpawnState = PipeSpawnStateMachineSOFSM.NoSpawn;
        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedStateSOFSM.ZeroPipeSpeed;

        yield return null;
    }

    /// <summary>
    /// Method to be invoked by GameEventlistener subscribed to First Jump GameEvent. 
    /// JumpScript Script calls GameEvent on first jump after starting the game.
    /// </summary>
    public void FirstJump()
    {
        StopCO(GameCoroutine);

        GameCoroutine = StartCoroutine(Normal());


    }

    /// <summary>
    /// Method to be invoked by GameEventListener subscribed to SpeedBoostTrigger GameEvent
    /// BoostCollider calls GameEvent if BoostCollider OnTriggerEnter with Bord
    /// </summary>
    public void StartHaste()
    {
        StopCO(GameCoroutine);
        GameCoroutine = StartCoroutine(Haste());
    }

    public IEnumerator Normal()
    {
        _gameState.GameState = GameStateSOFSM.Normal;


        _bordStateMachineSO.BordMainState = BordStateMachineSOFSM.RegularSpeedState;
        _pipeSpawnStatemachineSO.PipeSpawnState = PipeSpawnStateMachineSOFSM.RegularSpeedSpawn;
        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedStateSOFSM.RegularPipeSpeed;

        yield return null;
    }

    public IEnumerator Haste()
    {
        _gameState.GameState = GameStateSOFSM.Haste;

        _bordStateMachineSO.BordMainState = BordStateMachineSOFSM.BoostSpeedState;
        _pipeSpeedStateSO.PipeSpeedState = PipeSpeedStateSOFSM.AccelPipeSpeed;
        _pipeSpawnStatemachineSO.PipeSpawnState = PipeSpawnStateMachineSOFSM.AccelPipeSpawn;

        yield return null;
    }

    
    private void StopCO(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }



/*



                    // Called when speed boost event triggers
                    public void OnSpeedBoostTriggered()
                    {
                        Debug.Log("GameManager: Speed Boost triggered.");

                        // Tell PipeSpeedLogic to go into accel speed state
                        pipeSpeedLogic.StateChangerAccelSpeed();

                        // You could also trigger spawn logic changes or other stuff here
                    }

                    // Called when speed decel event triggers
                    public void OnSpeedDecelTriggered()
                    {
                        Debug.Log("GameManager: Speed Decel triggered.");

                        pipeSpeedLogic.StateChangerDecel();

                        // Maybe slow down pipe spawning or whatever
                    }

                    // Called when regular speed resumes
                    public void OnRegularSpeedTriggered()
                    {
                        Debug.Log("GameManager: Regular speed resumed.");

                        pipeSpeedLogic.StateChangerRegularSpeed();
                    }

                    // Called when boost collider detects collision
                    public void OnBoostColliderTriggered()
                    {
                        Debug.Log("GameManager: Boost Collider triggered.");

                        // Could trigger speed boost or any other game reaction here
                        pipeSpeedLogic.StateChangerAccelSpeed();
                    }

                    // Add more public handlers for other events as needed

                    // Optional initialization or state management if needed
                    void Start()
                    {
                        // Could initialize default states here if you want
                        if (pipeSpeedLogic != null)
                            pipeSpeedLogic.StateChangerRegularSpeed();
                    }

                    */
}
