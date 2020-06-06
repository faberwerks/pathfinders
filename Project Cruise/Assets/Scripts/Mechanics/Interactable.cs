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

    public bool canAddInteractListener()
    {
        Debug.Log(characters.Count);
        if (characters.Count == 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Virtual method to check if the interact listener can be removed.
    /// </summary>
    public bool canRemoveInteractListener()
    {
        if (characters.Count == 0)
        {
            return true;
        }

        return false;
    }

    protected void addCharacter(Collider2D collision)
    {
        if (characters.Count == 0)
        {
            button.onClick.AddListener(Interact);
            Debug.Log("LISTENER ADDED!");
        }
        addCharacterWithoutListener(collision);
        
    }

    protected void addCharacterWithoutListener(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            characters.Add(collision.transform);
            Debug.Log("CHARACTER ADDED!");
        }
    }

    protected void removeCharacter(Collider2D collision)
    {
        removeCharacterWithoutListener(collision);
        if (characters.Count == 0)
        {
            button.onClick.RemoveListener(Interact);
        }
    }

    protected void removeCharacterWithoutListener(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            characters.Remove(collision.transform);
        }
    }
}
