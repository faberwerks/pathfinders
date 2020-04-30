using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to handle teleportation.
/// </summary>
public class Teleporter : Interactable
{
    private List<Transform> characters; // characters to be teleported

    public Transform target;            // target to teleport characters to

    // Start is called before the first frame update
    private void Start()
    {
        characters = new List<Transform>();
    }

    public override void Interact()
    {
        if (target && characters.Count > 0)
        {
            Blackboard.Instance.LevelManager.SaveCheckpoint();
            Teleport();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            characters.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagStrings.PLAYER_TAG))
        {
            characters.Remove(collision.transform);
        }
    }

    /// <summary>
    /// Teleports all detected characters to target position.
    /// </summary>
    private void Teleport()
    {
        foreach (Transform character in characters)
        {
            character.position = target.position;
        }
    }

}
