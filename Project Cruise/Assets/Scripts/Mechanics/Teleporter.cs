using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to handle teleportation.
/// </summary>
public class Teleporter : Interactable
{
    // private List<Transform> characters; // characters to be teleported

    public Transform target;            // target to teleport characters to

    float yOffset = -0.35f;

    private Vector3 offset;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        offset = new Vector3(0, yOffset, 0);
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
        addCharacter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        removeCharacter(collision);
    }

    /// <summary>
    /// Teleports all detected characters to target position.
    /// </summary>
    private void Teleport()
    {
        foreach (Transform character in characters)
        {
            character.position = target.position + offset;
        }
    }

}
