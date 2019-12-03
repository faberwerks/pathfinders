using UnityEngine;

/// <summary>
/// A component for key-triggered doors.
/// </summary>
public class Door : Interactable
{
    [SerializeField]
    private int id = 0;
    public int ID { get { return id; } }

    public override void Interact()
    {
        Destroy(gameObject);
    }
}
