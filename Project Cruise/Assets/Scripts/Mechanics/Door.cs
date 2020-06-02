using UnityEngine;

[RequireComponent(typeof(AudioHandler))]
/// <summary>
/// A component for key-triggered doors.
/// </summary>
public class Door : Interactable
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private AudioClip unlockSound;
    private AudioHandler audioHandler;

    public int ID { get { return id; } }

    private void Awake()
    {
        audioHandler = GetComponent<AudioHandler>();
    }

    public override void Interact()
    {
        Blackboard.Instance.LevelManager.SaveCheckpoint();
        if (audioHandler && unlockSound)
        {
            audioHandler.Play(unlockSound);
        }
        Destroy(gameObject);
    }
}
