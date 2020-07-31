using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base class for Interactable objects.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    protected List<Transform> characters;

    protected Button button;

    protected virtual void Start()
    {
        button = Blackboard.Instance.Button;

        characters = new List<Transform>();
    }

    /// <summary>
    /// Virtual method to handle interaction.
    /// </summary>
    public abstract void Interact();

    /// <summary>
    /// Virtual method to check if there are no characters left in the interactable.
    /// </summary>
    public bool NoCharacters()
    {
        if (characters.Count == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds listener and adds character to track.
    /// </summary>
    /// <param name="collision">Character collision data.</param>
    protected void AddCharacter(Collider2D collision)
    {
        if (characters.Count == 0)
        {
            button.onClick.AddListener(Interact);
        }
        AddCharacterWithoutListener(collision);
        
    }

    /// <summary>
    /// Adds character to track.
    /// </summary>
    /// <param name="collision">Character collision data.</param>
    protected void AddCharacterWithoutListener(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            characters.Add(collision.transform);
        }
    }

    /// <summary>
    /// Removes character from tracking and removes listener if there are no characters left.
    /// </summary>
    /// <param name="collision">Character collision data.</param>
    protected void RemoveCharacter(Collider2D collision)
    {
        RemoveCharacterWithoutListener(collision);
        if (characters.Count == 0)
        {
            button.onClick.RemoveListener(Interact);
        }
    }

    /// <summary>
    /// Removes character from tracking.
    /// </summary>
    /// <param name="collision">Character collision data.</param>
    protected void RemoveCharacterWithoutListener(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            characters.Remove(collision.transform);
        }
    }
}
