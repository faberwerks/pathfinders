using UnityEngine;

public class Trap : MonoBehaviour
{
    public AudioClip hitSound;

    private AudioHandler audioHandler;

    private void Start()
    {
        audioHandler = GetComponent<AudioHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagStrings.PLAYER_TAG))
        {
            audioHandler.Play(hitSound);
            collision.gameObject.GetComponent<Animator>().SetTrigger("Die");
            Blackboard.Instance.LevelManager.DisableCharacterMovement();
        }
    }
}
