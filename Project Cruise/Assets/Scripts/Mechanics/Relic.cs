using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : Interactable
{
    public override void Interact()
    {
        Blackboard.Instance.LevelManager.RelicCollected = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        addCharacter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        removeCharacter(collision);
    }
}
