using UnityEngine;

/// <summary>
/// Component to handle relic in wall behaviour.
/// </summary>
public class Relic : Interactable
{
    protected override void Start()
    {
        base.Start();

        if (LevelDirectory.Instance.GetLevelData(GameData.Instance.currLevelID).hasRelic)
        {
            if (GameData.Instance.saveData.levelSaveData[GameData.Instance.currLevelID - 1].hasFoundRelic)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void Interact()
    {
        Blackboard.Instance.LevelManager.RelicCollected = true;
        Blackboard.Instance.LevelManager.relicNotification.SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddCharacter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveCharacter(collision);
    }
}
