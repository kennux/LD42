using UnityEngine;
using UnityTK.AssetManagement;

/// <summary>
/// Abstract base class for implementing game events.
/// </summary>
public abstract class GameEvent : ManagedScriptableObject
{
    /// <summary>
    /// The difficulty rating of the event.
    /// </summary>
    public EventDifficulty difficulty;

    /// <summary>
    /// Executes the events.
    /// This method should manipulate the game state in some way..
    /// </summary>
    public abstract void Execute();
}