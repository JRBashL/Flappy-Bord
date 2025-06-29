using UnityEngine;

public class BordScore : MonoBehaviour
{

    [SerializeField]
    private int _bordScore;
    [SerializeField]
    private int _savedScore;
    [SerializeField]
    private int _bestScore;
    private GameEvent BroadcastScoreGameEvent, AddScoreGameEvent;
    [SerializeField]
    private bool _isCountingScore;

    void Awake()
    {
        _bordScore = 0;
        _savedScore = 0;
        _isCountingScore = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCountingScore)
        {
            _bordScore += 1;
            BroadcastScoreGameEvent.TriggerEvent();
        }
    }



    public void Addscore(int ScoretoAdd)
    {
        _bordScore += ScoretoAdd;
        AddScoreGameEvent.TriggerEvent();
    }

    public void SaveScore()
    {
        _savedScore = _bordScore;
    }

    public void SaveBestScore()
    {
        _bestScore = _bordScore;
    }
    
    public void StopCountingScore()
    {
        _isCountingScore = false;
    }

    public void StartCountingScore()
    {
        _isCountingScore = true;
    }

}
