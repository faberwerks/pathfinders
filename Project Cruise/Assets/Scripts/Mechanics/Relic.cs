using UnityEngine;

/// <summary>
/// Component to handle relic in wall behaviour.
/// </summary>
public class Relic : Interactable
{
    public override void Interact()
    {
        Blackboard.Instance.LevelManager.RelicCollected = true;
        Blackboard.Instance.LevelManager.relicNotification.SetActive(true);
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
