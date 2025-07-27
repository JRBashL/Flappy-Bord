using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    // The GameEvent class is from the scriptable object class. This is declared but you make the reference in the 
    // inspector. This monobehaviour is then attached to the GameObject that will listen to that certain game event.
    public GameEvent GameEvent;

    // No param
    [Header("Static Call Events")]
    public UnityEvent onEventTriggered;

    // 1 param
    [Header("One Param Dynamic Call Events")]
    public UnityEvent<float> onEventTriggeredOneFloat;
    public UnityEvent<int> onEventTriggeredOneInt;
    public UnityEvent<bool> onEventTriggeredOneBool;
    public UnityEvent<string> onEventTriggeredOneString;

    // 2 params
    [Header("Two Params Dynamic Call Events")]
    public UnityEvent<float, float> onEventTriggeredTwoFloat;
    public UnityEvent<int, int> onEventTriggeredTwoInt;
    public UnityEvent<bool, bool> onEventTriggeredTwoBool;
    public UnityEvent<string, string> onEventTriggeredTwoString;

    // 3 params
    [Header("Three Params Dynamic Call Events")]
    public UnityEvent<float, float, float> onEventTriggeredThreeFloat;
    public UnityEvent<int, int, int> onEventTriggeredThreeInt;
    public UnityEvent<bool, bool, bool> onEventTriggeredThreeBool;
    public UnityEvent<string, string, string> onEventTriggeredThreeString;

    // 4 params
    [Header("Four Params Dynamic Call Events")]
    public UnityEvent<float, float, float, float> onEventTriggeredFourFloat;
    public UnityEvent<int, int, int, int> onEventTriggeredFourInt;
    public UnityEvent<bool, bool, bool, bool> onEventTriggeredFourBool;
    public UnityEvent<string, string, string, string> onEventTriggeredFourString;


    // Subscribes to the game event when enabled
    void OnEnable()
    {
        GameEvent.AddListener(this);
    }

    // Unsubscribes to the game event when disabled
    void OnDisable()
    {
        GameEvent.RemoveListener(this);
    }

    public void OnEventTriggered()
    {
        // This invoke method is part of the class UnityEvent and in the inspector you can add other methods to this.
        // If I understand it correctly, this is the "I hear you!" and then the methods that are attached to the inspector
        // is the response.
        onEventTriggered.Invoke();
    }

    // 1 param invokers
    public void OnEventTriggeredOneFloat(float data)
    {
        onEventTriggeredOneFloat?.Invoke(data);
    }

    public void OnEventTriggeredOneInt(int data)
    {
        onEventTriggeredOneInt?.Invoke(data);
    }

    public void OnEventTriggeredOneBool(bool data)
    {
        onEventTriggeredOneBool?.Invoke(data);
    }

    public void OnEventTriggeredOneString(string data)
    {
        onEventTriggeredOneString?.Invoke(data);
    }

    // 2 params invokers
    public void OnEventTriggeredTwoFloat(float data1, float data2)
    {
        onEventTriggeredTwoFloat?.Invoke(data1, data2);
    }

    public void OnEventTriggeredTwoInt(int data1, int data2)
    {
        onEventTriggeredTwoInt?.Invoke(data1, data2);
    }

    public void OnEventTriggeredTwoBool(bool data1, bool data2)
    {
        onEventTriggeredTwoBool?.Invoke(data1, data2);
    }

    public void OnEventTriggeredTwoString(string data1, string data2)
    {
        onEventTriggeredTwoString?.Invoke(data1, data2);
    }

    // 3 params invokers
    public void OnEventTriggeredThreeFloat(float data1, float data2, float data3)
    {
        onEventTriggeredThreeFloat?.Invoke(data1, data2, data3);
    }

    public void OnEventTriggeredThreeInt(int data1, int data2, int data3)
    {
        onEventTriggeredThreeInt?.Invoke(data1, data2, data3);
    }

    public void OnEventTriggeredThreeBool(bool data1, bool data2, bool data3)
    {
        onEventTriggeredThreeBool?.Invoke(data1, data2, data3);
    }

    public void OnEventTriggeredThreeString(string data1, string data2, string data3)
    {
        onEventTriggeredThreeString?.Invoke(data1, data2, data3);
    }

    // 4 params invokers
    public void OnEventTriggeredFourFloat(float data1, float data2, float data3, float data4)
    {
        onEventTriggeredFourFloat?.Invoke(data1, data2, data3, data4);
    }

    public void OnEventTriggeredFourInt(int data1, int data2, int data3, int data4)
    {
        onEventTriggeredFourInt?.Invoke(data1, data2, data3, data4);
    }

    public void OnEventTriggeredFourBool(bool data1, bool data2, bool data3, bool data4)
    {
        onEventTriggeredFourBool?.Invoke(data1, data2, data3, data4);
    }

    public void OnEventTriggeredFourString(string data1, string data2, string data3, string data4)
    {
        onEventTriggeredFourString?.Invoke(data1, data2, data3, data4);
    }
}
