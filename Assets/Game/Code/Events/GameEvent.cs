using UnityEngine;
using UnityTK.AssetManagement;

/// <summary>
/// Abstract base class for implementing game events.
/// </summary>
public abstract class GameEvent : ManagedScriptableObject
{
    /// <summary>
    /// The text shown in the event notification.
    /// </summary>
    public string uiText;

    /// <summary>
    /// Executes the events.
    /// This method should manipulate the game state in some way..
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// Returns the spawn probability under the current circumstances.
    /// </summary>
    /// <returns>0-1</returns>
    public abstract float GetSpawnProbability();
}