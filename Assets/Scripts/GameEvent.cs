using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    // Creates a list of type GameEventListener for the subscribers of the game event.
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    // 0 params
    /// <summary>
    /// Trigger Event to invoke all listeners with no parameters.
    /// </summary>
    public void TriggerEvent()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered();
        }
    }

    // 1 param
    /// <summary>
    /// Trigger Event to invoke listeners with one float.
    /// </summary>
    /// <param name="data">The float value to send to listeners.</param>
    public void TriggerEventOneFloat(float data)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredOneFloat(data);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with one int.
    /// </summary>
    /// <param name="data">The int value to send to listeners.</param>
    public void TriggerEventOneInt(int data)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredOneInt(data);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with one bool.
    /// </summary>
    /// <param name="data">The bool value to send to listeners.</param>
    public void TriggerEventOneBool(bool data)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredOneBool(data);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with one string.
    /// </summary>
    /// <param name="data">The string value to send to listeners.</param>
    public void TriggerEventOneString(string data)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredOneString(data);
        }
    }

    // 2 params
    /// <summary>
    /// Trigger Event to invoke listeners with two floats.
    /// </summary>
    /// <param name="data1">The first float value.</param>
    /// <param name="data2">The second float value.</param>
    public void TriggerEventTwoFloat(float data1, float data2)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredTwoFloat(data1, data2);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with two ints.
    /// </summary>
    /// <param name="data1">The first int value.</param>
    /// <param name="data2">The second int value.</param>
    public void TriggerEventTwoInt(int data1, int data2)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredTwoInt(data1, data2);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with two bools.
    /// </summary>
    /// <param name="data1">The first bool value.</param>
    /// <param name="data2">The second bool value.</param>
    public void TriggerEventTwoBool(bool data1, bool data2)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredTwoBool(data1, data2);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with two strings.
    /// </summary>
    /// <param name="data1">The first string value.</param>
    /// <param name="data2">The second string value.</param>
    public void TriggerEventTwoString(string data1, string data2)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredTwoString(data1, data2);
        }
    }

    // 3 params
    /// <summary>
    /// Trigger Event to invoke listeners with three floats.
    /// </summary>
    /// <param name="data1">The first float value.</param>
    /// <param name="data2">The second float value.</param>
    /// <param name="data3">The third float value.</param>
    public void TriggerEventThreeFloat(float data1, float data2, float data3)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredThreeFloat(data1, data2, data3);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with three ints.
    /// </summary>
    /// <param name="data1">The first int value.</param>
    /// <param name="data2">The second int value.</param>
    /// <param name="data3">The third int value.</param>
    public void TriggerEventThreeInt(int data1, int data2, int data3)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredThreeInt(data1, data2, data3);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with three bools.
    /// </summary>
    /// <param name="data1">The first bool value.</param>
    /// <param name="data2">The second bool value.</param>
    /// <param name="data3">The third bool value.</param>
    public void TriggerEventThreeBool(bool data1, bool data2, bool data3)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredThreeBool(data1, data2, data3);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with three strings.
    /// </summary>
    /// <param name="data1">The first string value.</param>
    /// <param name="data2">The second string value.</param>
    /// <param name="data3">The third string value.</param>
    public void TriggerEventThreeString(string data1, string data2, string data3)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredThreeString(data1, data2, data3);
        }
    }

    // 4 params
    /// <summary>
    /// Trigger Event to invoke listeners with four floats.
    /// </summary>
    /// <param name="data1">The first float value.</param>
    /// <param name="data2">The second float value.</param>
    /// <param name="data3">The third float value.</param>
    /// <param name="data4">The fourth float value.</param>
    public void TriggerEventFourFloat(float data1, float data2, float data3, float data4)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredFourFloat(data1, data2, data3, data4);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with four ints.
    /// </summary>
    /// <param name="data1">The first int value.</param>
    /// <param name="data2">The second int value.</param>
    /// <param name="data3">The third int value.</param>
    /// <param name="data4">The fourth int value.</param>
    public void TriggerEventFourInt(int data1, int data2, int data3, int data4)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredFourInt(data1, data2, data3, data4);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with four bools.
    /// </summary>
    /// <param name="data1">The first bool value.</param>
    /// <param name="data2">The second bool value.</param>
    /// <param name="data3">The third bool value.</param>
    /// <param name="data4">The fourth bool value.</param>
    public void TriggerEventFourBool(bool data1, bool data2, bool data3, bool data4)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredFourBool(data1, data2, data3, data4);
        }
    }

    /// <summary>
    /// Trigger Event to invoke listeners with four strings.
    /// </summary>
    /// <param name="data1">The first string value.</param>
    /// <param name="data2">The second string value.</param>
    /// <param name="data3">The third string value.</param>
    /// <param name="data4">The fourth string value.</param>
    public void TriggerEventFourString(string data1, string data2, string data3, string data4)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredFourString(data1, data2, data3, data4);
        }
    }

    //Method for subscribing to the game event
    public void AddListener(GameEventListener Listener)
    {
        _listeners.Add(Listener);
    }

    //Method for unsubscribing to the game event

    public void RemoveListener(GameEventListener Listener)
    {
        _listeners.Remove(Listener);
    }


}
