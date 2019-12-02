using UnityEngine;

/// <summary>
/// Base class for Interactable objects.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    /// <summary>
    /// Virtual method to handle interaction.
    /// </summary>
    public abstract void Interact();
}
