using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    // Creates a list of type GameEventListener for the subscribers of the game event.
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    // This is a method that runs through the list of subscribers and calls OnEventTriggered for all of them
    // There a reason of them going it through backwards though I'm not sure why and it has something to do with
    // the subscribers unsubscribing. This is the delegate, essentially. It's the thing that causes the shout to the
    // listeners. I think I'm supposed to create an instance of this class in my script, and use the
    // InstanceName.OnEventTriggered() method for it to call out.
    public void TriggerEvent()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered();
        }
    }

    /// <summary>
    /// Trigger Event to Invoke listeners with one float number.
    /// </summary>
    /// <param name="data">The float number to send through the Invoke.</param>
    public void TriggerEventOneFloat(float data)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredOneFloat(data);
        }
    }

    /// <summary>
    /// Trigger Event to Invoke listeners with two float numbers.
    /// </summary>
    /// <param name="data1">The first float number to send through the Invoke.</param>
    /// <param name="data2">The second float number to send through the Invoke.</param>
    public void TriggerEventTwoFloat(float data1, float data2)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredTwoFloat(data1, data2);
        }
    }

    /// <summary>
    /// Trigger Event to Invoke listeners with three float numbers.
    /// </summary>
    /// <param name="data1">The first float number to send through the Invoke.</param>
    /// <param name="data2">The second float number to send through the Invoke.</param>
    /// <param name="data3">The second float number to send through the Invoke.</param>
    public void TriggerEventThreeFloat(float data1, float data2, float data3)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredThreeFloat(data1, data2, data3);
        }
    }

    /// <summary>
    /// Trigger Event to Invoke listeners with four float numbers.
    /// </summary>
    /// <param name="data1">The first float number to send through the Invoke.</param>
    /// <param name="data2">The second float number to send through the Invoke.</param>
    /// <param name="data3">The second float number to send through the Invoke.</param>
    /// <param name="data4">The second float number to send through the Invoke.</param>
    public void TriggerEventFourFloat(float data1, float data2, float data3, float data4)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggeredFourFloat(data1, data2, data3, data4);
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
