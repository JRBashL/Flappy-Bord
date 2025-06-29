using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    // The GameEvent class is from the scriptable object class. This is declared but you make the reference in the 
    // inspector. This monobehaviour is then attached to the GameObject that will listen to that certain game event.
    public GameEvent GameEvent;
    public UnityEvent onEventTriggered;

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
}
