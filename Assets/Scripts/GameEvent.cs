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
