using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeSpawnLogic : MonoBehaviour
{

    // Region for declaring fields related to the Object Pooling
    #region 

    // Declare integer for pipe object pooling. The size of the pool can be determined in the inspector.
    [SerializeField] private int _pipePoolSize;

    // Declare a list to get a reference for all of the objects in the pool
    List<GameObject> _pipePoolList;

    // Declare GameObject prefab that the pipes spawn from.        
    [SerializeField] private GameObject _prefabPipes1;
    [SerializeField] private GameObject _prefabPipes2;
    [SerializeField] private GameObject _prefabPipes3;

    // Declare the weights for each one
    [SerializeField] private float _prefabPipes1SpawnRate;
    [SerializeField] private float _prefabPipes2SpawnRate;
    [SerializeField] private float _prefabPipes3SpawnRate;


    #endregion

    // Region for delcaring fields related to Pipe Spawning
    #region 

    // Declare a speed of the pipes
    [SerializeField] private float _pipeSpeed;

    // Declare how far away the pipes spawn at
    [SerializeField] private float _pipeSpawnDistance;

    // Declare the pipe spawn timer
    [SerializeField] private float _pipeSpawnTimer;

    // Declare the lane gap and declare an array for the lanes.
    [SerializeField] private int _laneGap;
    int[] _laneArray;

    int[] _pipeTypeArray;
    List<GameObject> _pipes1PoolList;
    List<GameObject> _pipes2PoolList;
    List<GameObject> _pipes3PoolList;

    #endregion

    // clock for the timer 
    private float _clock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // At the start of this scene I want set a pool of pipes. The size is configurable in the inspector.
        _laneArray = new int[] { -_laneGap, 0, _laneGap };
        _pipes1PoolList = ObjectPool(_prefabPipes1, _pipePoolSize);
        _pipes2PoolList = ObjectPool(_prefabPipes2, _pipePoolSize);
        _pipes3PoolList = ObjectPool(_prefabPipes3, _pipePoolSize);

    }

    // Update is called once per frame
    void Update()
    {
        _clock += Time.deltaTime;

        if (_clock > _pipeSpawnTimer)
        {
            // Creates the variable height for the new set of pipes to spawn
            float[] yOffsetArray = new float[] { Random.Range(2f, 18f), Random.Range(-5f, 18f), Random.Range(-5f, 18f) };

            // Generates an array of which pipes (0, 1, 2) based on a weighted spawn rate.
            _pipeTypeArray = WeightedRNG(new int[] { 1, 1, 1 }, _prefabPipes1SpawnRate, _prefabPipes2SpawnRate, _prefabPipes3SpawnRate);
            // Debug.Log("The field _pipeTypeArray is " + _pipeTypeArray[0] + _pipeTypeArray[1] + _pipeTypeArray[2]);

            // FOR DEBUGGING
            //_pipeTypeArray = new int[]{0, 1, 2};

            // Spawns the pipes in the lane
            PipeSpawn(yOffsetArray);
            // Reset timer
            _clock = 0f;
        }

    }
    
    // Creates a pool of a configurable size and creates a list with a reference to each.
    private List<GameObject> ObjectPool(GameObject Prefab, int NumberToSpawn)
    {
        //Create List
        List<GameObject> ObjectPoolList = new List<GameObject>();

        for (int i = 0; i < NumberToSpawn; i++)
        {
            // The coordinates are hard coded as x = 50, 55, 60, 65, ..)
            ObjectPoolList.Add(Instantiate(Prefab, new Vector3(50 + 5 * i, 0, -50f), Quaternion.identity));
            ObjectPoolList[i].SetActive(false);
        }

        return ObjectPoolList;
    }



    /// <summary>
    /// Method that ouputs an array of what to spawn depending on the weights. The first input is an array that is a bad combo that should be avoided. In this use 
    /// case, a bad combo of all pipes with no gap is bad combo that should never be spawned and therefore should not be outputted
    /// The second input is an array of weights for variable length. The weights can be inputed in percent or decimals.
    /// I wrote it in a flexible way to learn. So params float[] weightArray mean that the compiler creates an array called weightArray
    /// with the inputs when the method is called i.e. WeightedRNG(x,y,z,....) starts an array weightArray with the arguments passed in
    /// The weightArray arguments are in percent so enter 10, 20, 70 to represent 10%, 20%, and 70%
    /// </summary>
    /// <param name="badCombo">The first input is an array that is a bad combo that should be avoided.</param>
    /// <param name="weightArray">The second input is an array of weights for variable length. The weights can be in percent or decimals.</param>
    /// <returns></returns>
    private int[] WeightedRNG(int[] badCombo, params float[] weightArray)
    {
        int[] result = new int[weightArray.Length];

        //Error handling. Returns an empty array if the badCombo array length isn't the same as the weight list length
        if (badCombo.Length != weightArray.Length)
        {
            return result;
        }

        // Creates a field to store the total of the weights. This is to handle error correction when sum of weights entered
        // is more than 1
        float weightTotal = 0;
        foreach (float entry in weightArray)
        {
            weightTotal += entry;
        }

        do
        {
            // outer for-loop is looping through each index of the result array and the length is variable depending on the args
            for (int i = 0; i < weightArray.Length; i++)
            {
                // Generates a random value for comparison to the weights
                float randomNumber = Random.Range(0, weightTotal);

                // Creates a cumulative sum to compare to for weight
                float sum = 0f;

                // Compares the random number against the weights
                // The visual analogy is the random number the carnival hammer game. The random number has been rolled and is the height
                // of the carnival hammer. Every step in this for loop checks the height of the hammer and the bars in the hammer meter
                // is the weights or the array in weightArray
                for (int j = 0; j < weightArray.Length; j++)
                {
                    sum += weightArray[j];

                    if (randomNumber <= sum)
                    {
                        result[i] = j;
                        break;
                    }
                }

            }
        }
        // while the badCombo array is the same as the result array say (1, 1, 1) = (1, 1, 1). This line stops the bad combo from outputting.
        while (badCombo.SequenceEqual(result));

        return result;
    }

    // Teleports a pipe to a lane. The distance away in the z direction is specified in the inspector
    private void PipeSpawn(
        //float PipeSpawnDistance,   // Spawn distance that is away in the z direction to get it to spawn off camera
        //int[] LaneArray,           // This is an array that holds the x position of the left, middle, and right lanes
        float[] YOffsetArray)      // This is to control the random placement of the gap between the pipes
        //float PipeSpeed)           // Pipes speed in the -z direction, going towards the player.
    {

        // This is an operation to check the list of gameobjects from the pool to see which is active. Then creates a new list of 3
        // inactive pipes that can be spawned.
        List<GameObject> listOfPipesToSpawn = new List<GameObject>();

        for (int index = 0; index < _pipeTypeArray.Length; index++)
        {
            listOfPipesToSpawn.Add(null);
        }

        List<GameObject> mediaryList = new List<GameObject>();



        for (int index = 0; index < _pipeTypeArray.Length; index++)
        {

            // will check which number is in the _pipeTypeArray and make that the mediary list. Since there are multiple pools
            // where each pool is a different pipe type. This for loop will assign the mediary list to the correct pool depending on
            // what the _pipeTypeArray shows. Example if _pipeTypeArray[1] = 1, that will be the that coressponded pipe poo1 

            if (_pipeTypeArray[index] == 0)
            {
                mediaryList = _pipes1PoolList;

            }

            else if (_pipeTypeArray[index] == 1)
            {
                mediaryList = _pipes2PoolList;
            }

            else
            {
                mediaryList = _pipes3PoolList;
            }


            // then once we're done checking for which pool to use by mediary list
            // we run through the pool and see which ones are disabled or not.          

            for (int i = 0; i < mediaryList.Count; i++)
            {
                if (mediaryList[i].activeInHierarchy == false)
                {
                    listOfPipesToSpawn[index] = mediaryList[i];
                    listOfPipesToSpawn[index].SetActive(true);
                    break;
                }
            }


        }

        //Debug.Log("The list for types to spawn is" + listOfPipesToSpawn[0] + listOfPipesToSpawn[1] + listOfPipesToSpawn[2]);

        // Once the new sublist is created. It spawns the first one in the list in lane one, the second one in the list in lane 2,
        // and the third one on the list in lane 3.
        for (int i = 0; i < 3; i++)
        {
            listOfPipesToSpawn[i].transform.position = new Vector3(_laneArray[i], YOffsetArray[i], _pipeSpawnDistance);
            listOfPipesToSpawn[i].transform.rotation = Quaternion.identity;
            
                 
            // Old line of code below where I set the speed of the pipe via a method. 
            // listOfPipesToSpawn[i].GetComponent<PipeScript>().SetPipeSpeed(_pipeSpeed);
        }
    }



}
